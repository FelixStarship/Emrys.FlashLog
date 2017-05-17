using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using TestLayout.RenderPartial;


namespace TestLayout.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {

            //var i = 10;
            //var j = 0;
            //var result = i / j;

            return View();
        }

        public string RenderPartial(string viewName)
        {
            var objects = new objects{ text = "测试" };
            var partialView = this.RenderPartialViewToString(viewName, objects);
            var json = JsonConvert.SerializeObject(partialView);
            return json;
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

    public class objects
    {
        public string text { get; set; }
    }
}