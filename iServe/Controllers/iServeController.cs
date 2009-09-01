using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iServe.Models.Security;
using iServe.Models;
using iServe.Models.F1APIServices;
using System.Web.Mvc;

namespace iServe.Controllers {
	public class iServeController : System.Web.Mvc.Controller {
		public iServeController() {
			Principal principal = System.Threading.Thread.CurrentPrincipal as Principal;

			if (principal != null) {
				_currentUser = principal.CurrentUser as User;
			}
		}

		public iServeController(User currentUser) {
			_currentUser = currentUser;
		}

		private User _currentUser = null;
		protected User CurrentUser {
			get { return _currentUser; }
		}
		
		#region OAuth properties
		public Token AccessToken {
			get {
				if (Session["AccessToken"] != null) {
					return (Token)Session["AccessToken"];
				}
				return null;
			}
			set {
				if (Session["AccessToken"] != null) {
					Session["AccessToken"] = value;
				}
				else {
					Session.Add("AccessToken", value);
				}
			}
		}
		public Token RequestToken {
			get {
				if (Session["RequestToken"] != null) {
					return (Token)Session["RequestToken"];
				}
				return null;
			}
			set {
				if (Session["RequestToken"] != null) {
					Session["RequestToken"] = value;
				}
				else {
					Session.Add("RequestToken", value);
				}
			}
		}
		#endregion OAuth properties

		protected override void OnActionExecuting(System.Web.Mvc.ActionExecutingContext filterContext) {
			User user = null;

			IServePrincipal principal = System.Threading.Thread.CurrentPrincipal as IServePrincipal;
			if (principal != null) {
				user = principal.CurrentUser as User;
			}
			ViewData["User"] = user;

			base.OnActionExecuting(filterContext);
		}

		public int? GetCurrentUserID() {
			int? currentUserID = null;

			if (CurrentUser != null) {
				currentUserID = CurrentUser.ID;
			}

			return currentUserID;
		}

		protected string GetRequestedChurchCode() {
			string hostname = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_HOST"];
			// Get church code from hostname
			int index = hostname.IndexOf(".");

			if (index <= 0) {
				index = 0;
			}
			string churchCode = hostname.Substring(0, index).ToLower();

			if (churchCode.Length == 0 || churchCode == Config.DomainName) {
				churchCode = Config.DefaultChurchCode;
			}
			if (string.IsNullOrEmpty(churchCode) || hostname == Config.DomainName) {
				churchCode = "www";
			}

			return churchCode;
		}

	}
}
