using Emrys.FlashLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace TestLayout
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //写日志
            FlashLogger.Instance().Register();
        }

        protected void Application_Error()
        {
            var lasterror = Server.GetLastError();
            if (lasterror != null)
            {
                var httpError = lasterror as HttpException;
                if (httpError != null)
                {
                    switch (httpError.GetHttpCode())
                    {
                        case 404:
                            Response.Redirect("/PageNotFound.html");
                            break;
                        case 500:
                            Response.Redirect("/ErrorPage.html");
                            break;
                            
                    }
                }
            }
        }
    }
}
