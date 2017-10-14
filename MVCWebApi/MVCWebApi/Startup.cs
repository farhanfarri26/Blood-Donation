using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVCWebApi.Startup))]
namespace MVCWebApi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
