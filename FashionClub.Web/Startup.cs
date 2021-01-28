using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FashionClub.Web.Startup))]
namespace FashionClub.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
