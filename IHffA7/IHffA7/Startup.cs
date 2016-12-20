using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IHffA7.Startup))]
namespace IHffA7
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
