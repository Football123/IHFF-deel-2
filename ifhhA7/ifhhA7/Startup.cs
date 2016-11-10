using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ifhhA7.Startup))]
namespace ifhhA7
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
