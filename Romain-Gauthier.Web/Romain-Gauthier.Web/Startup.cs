using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Romain_Gauthier.Web.Startup))]
namespace Romain_Gauthier.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
