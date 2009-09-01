using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iServe.Models;

namespace iServe.Controllers {
	[HandleError]
	public class HomeController : iServeController {
		public ActionResult Index() {
			ViewData["Message"] = "Welcome to ASP.NET MVC!";

			return View();
		}

		public ActionResult About() {
			return View();
		}

		public ActionResult Test() {
			Widget widget = new Widget();

			return View(widget);
		}

		public ActionResult Create(Widget widget) {
			if (ModelState.IsValid) {
				int i = 0;
			}

			return View("Test", widget);
		}
	}

}
