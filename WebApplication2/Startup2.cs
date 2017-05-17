using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(WebApplication2.Startup2))]

namespace WebApplication2
{
    public class Startup2
    {
        public void Configuration(IAppBuilder app)
        {
            // 有关如何配置应用程序的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkID=316888
            //app.MapSignalR<MyConnection1>("/myconnection");
            app.MapSignalR();
        }
    }
}
