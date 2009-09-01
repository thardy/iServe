using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace iServe.Controllers
{
    /// <summary>
    /// This controller interfaces with the FellowshipOne Individual
    /// </summary>
	public class UserController : iServeController
    {
        //
        // GET: /User/

        public ActionResult Index()
        {
            return View();
        }

    }
}
