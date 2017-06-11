using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(log4netTest.Startup))]
namespace log4netTest
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
