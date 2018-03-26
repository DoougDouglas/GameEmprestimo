using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GameEmprestimo.Startup))]
namespace GameEmprestimo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
