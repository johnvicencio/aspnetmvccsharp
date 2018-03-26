using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GreatCRM.Startup))]
namespace GreatCRM
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
