using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TestLayout.Startup))]
namespace TestLayout
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
