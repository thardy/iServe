using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace iServe.Controllers
{
    public class MessageController : iServeController {
        public ActionResult New(int needID, int fromUserID) {
			ViewData["NeedID"] = needID;
			ViewData["FromUserID"] = fromUserID;

			return View();
        }

    }
}
