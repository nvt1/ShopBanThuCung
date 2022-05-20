using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NobiShop.Startup))]
namespace NobiShop
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
