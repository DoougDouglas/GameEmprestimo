using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Projeto.Web.Startup))]
namespace Projeto.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
