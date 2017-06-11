using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.IO;
using System.Threading;
using log4netTest.Filter;
using log4net;

namespace log4netTest
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {   
            
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


            //
            var configFile = new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log4net.config"));
            log4net.Config.XmlConfigurator.Configure(configFile);
            if (!configFile.Exists)
            {
                throw new Exception("未配置log4net.config文件!");
            }
            ThreadPool.QueueUserWorkItem(t => 
            {
                while (true)
                {
                    if (MyErrorAttribute.ExceptionQueue.Count > 0)
                    {
                        Exception ex = MyErrorAttribute.ExceptionQueue.Dequeue();
                        if (ex != null)
                        {
                            ILog logger = LogManager.GetLogger(ex.Source);
                            logger.Error(ex);
                        }
                        else
                        {
                            Thread.Sleep(50);
                        }
                    }
                    else
                    {
                        Thread.Sleep(50);
                    }
                }
            });
        }
    }
}
