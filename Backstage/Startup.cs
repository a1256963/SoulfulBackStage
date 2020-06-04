using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Backstage.Startup))]
namespace Backstage
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
