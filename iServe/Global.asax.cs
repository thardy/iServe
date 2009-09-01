using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Web.Mvc.DataAnnotations;
using System.Security.Principal;
using iServe.Models;
using System.Web.Security;
using iServe.Models.Security;

namespace iServe {
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication {
        public static void RegisterRoutes(RouteCollection routes) {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
			routes.IgnoreRoute("{*favicon}", new { favicon = @"(.*/)?favicon.ico(/.*)?" });

			//routes.MapRoute(
			//    "NeedIndex",									// Route name
			//    "Need/Index/{page}",							// URL with parameters
			//    new { controller = "Need", action = "Index", page=0 }  // Parameter defaults
			//);
			routes.MapRoute(
				"DefaultCreate",                                // Route name
				"{controller}/Create",                          // URL with parameters
				new { controller = "Home", action = "Create" }  // Parameter defaults
			);
			routes.MapRoute(
                "UserProfile",
                "UserProfile/{action}/{id}",
                new { controller = "Person", action = "Show", id = "" }
            );
            routes.MapRoute(
                "ShowInvolvement",                                              // Route name
                "Need/ShowInvolvement/{needID}",                           // URL with parameters
                new { controller = "Need", action = "ShowInvolvement" }  // Parameter defaults
            );
            routes.MapRoute(
                "Default",                                              // Route name
                "{controller}/{action}/{id}",                           // URL with parameters
                new { controller = "Need", action = "Index", id = "" }  // Parameter defaults
            );
        }

		protected void Application_Start() {
			RegisterRoutes(RouteTable.Routes);
			ModelBinders.Binders.DefaultBinder = new iServe.Models.dotNailsCommon.DotNailsModelBinder();
			//System.Web.Mvc.ModelBinders.Binders.DefaultBinder = (System.Web.Mvc.IModelBinder)new DataAnnotationsModelBinder();
		}

		protected void Application_AuthenticateRequest(object sender, EventArgs e) {
			if (!(HttpContext.Current.User == null)) {
				if (HttpContext.Current.User.Identity.IsAuthenticated) {
					// Extract the forms authentication cookie
					HttpCookie authCookie = Context.Request.Cookies[FormsAuthentication.FormsCookieName];
					FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
					
					// Get the UserID out of the cookie
					User user = new User(authTicket.UserData);

					Principal principal = new Principal(HttpContext.Current.User.Identity, user);
					HttpContext.Current.User = principal;
					System.Threading.Thread.CurrentPrincipal = principal;
				}
			}

		}

		protected void Application_AcquireRequestState(object sender, System.EventArgs e) {
            //iServe.Models.Security.Principal principal = new iServe.Models.Security.Principal(new GenericIdentity("Generic"), new iServe.Models.User { ID = 19059871, Name = "HardcodedUser" });
		
			//System.Web.HttpContext.Current.User = principal;
			//System.Threading.Thread.CurrentPrincipal = principal;
		}

	}
}