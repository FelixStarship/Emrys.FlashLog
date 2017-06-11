using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace log4netTest.Filter
{
    public class MyErrorAttribute:HandleErrorAttribute
    {
        public static Queue<Exception> ExceptionQueue = new Queue<Exception>();
        public override void OnException(ExceptionContext filterContext)
        {
            ExceptionQueue.Enqueue(filterContext.Exception);
        }
    }
}