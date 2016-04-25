using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Isdg.Startup))]
namespace Isdg
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
