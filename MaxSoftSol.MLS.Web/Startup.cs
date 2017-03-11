using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MaxSoftSol.MLS.Web.Startup))]
namespace MaxSoftSol.MLS.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
