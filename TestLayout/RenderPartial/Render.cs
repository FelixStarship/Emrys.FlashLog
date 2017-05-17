using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace TestLayout.RenderPartial
{
    public class BaseController:Controller
    {
        public  virtual string RenderPartialViewToString(string viewName, object model)
        {
            if (string.IsNullOrWhiteSpace(viewName))
                viewName = this.ControllerContext.RouteData.GetRequiredString("action");
            this.ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(this.ControllerContext, viewName);
                var viewContext = new ViewContext(this.ControllerContext, viewResult.View, this.ViewData, this.TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(viewContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }
    }
}