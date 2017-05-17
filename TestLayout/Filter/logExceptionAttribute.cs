using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Emrys.FlashLog;

namespace TestLayout.Filter
{
    //http://www.tuicool.com/articles/AZzQjy7
    public class logExceptionAttribute:HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            string controllerName = (string)filterContext.RouteData.Values["controller"];
            string actionName = (string)filterContext.RouteData.Values["action"];
            HandleErrorInfo info = new HandleErrorInfo(filterContext.Exception, controllerName, actionName);
            HttpRequestBase request = filterContext.RequestContext.HttpContext.Request;
            string broser = request.Browser.Browser;
            string broserVersion = request.Browser.Version;
            string system = request.Browser.Platform;
            string errorBaseInfo = $"Broser={broser},broserVersion={broserVersion},system={system},action={actionName},controller={controllerName}";
            //写入日志
            FlashLogger.Error(errorBaseInfo, filterContext.Exception);
            if (!filterContext.ExceptionHandled)
            {
                if (filterContext.HttpContext.IsCustomErrorEnabled)
                {
                    filterContext.HttpContext.Response.Clear();
                    HttpException httpex = filterContext.Exception as HttpException;
                    if (httpex != null)
                    {
                        filterContext.HttpContext.Response.StatusCode = httpex.GetHttpCode();
                    }
                    else
                    {
                        filterContext.HttpContext.Response.StatusCode = 500;
                    }
                    filterContext.Result = new ViewResult { ViewName = "/Views/Shared/Error.cshtml", ViewData = new ViewDataDictionary<HandleErrorInfo>(info) };
                    filterContext.ExceptionHandled = true;
                    filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
                }
                else
                {
                    //当customErrors=Off时
                    //当customErrors = RemoteOnly，且在本地调试时
                    filterContext.HttpContext.Response.Redirect("/ErrorPage.html");
                    filterContext.ExceptionHandled = true;
                    filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
                }

            }        
            base.OnException(filterContext);
        }
    }
}