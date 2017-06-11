using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace log4netTest.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            int result = 0;
            int x = 1, y = 0;
            result = x / y;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}