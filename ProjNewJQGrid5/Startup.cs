using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProjNewJQGrid5.Startup))]
namespace ProjNewJQGrid5
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
