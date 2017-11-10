using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BloodDonationMCVAPI.Startup))]
namespace BloodDonationMCVAPI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
