using Projeto.Data;
using Projeto.Data.Interfaces;
using Projeto.Domain;
using Projeto.Domain.Entities;
using Projeto.Domain.Interfaces;
using System.Web.Services.Description;

namespace Projeto.DependencyResolver
{
    public static class MvcResolver
    {
        public static void Init(ServiceCollection services, string connectionString)
        {
            services.AddDbContext<ProjetoContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ProjetoContext>()
                .AddDefaultTokenProviders();

            services.AddTransient<IAmigoBusiness, AmigoBusiness>();
            services.AddTransient<IAmigoData, AmigoData>();

            services.AddTransient<IConsoleBusiness, ConsoleBusiness>();
            services.AddTransient<IConsoleData, ConsoleData>();

            services.AddTransient<ITituloBusiness, TituloBusiness>();
            services.AddTransient<ITituloData, TituloData>();

            services.AddTransient<IEmprestimoBusiness, EmprestimoBusiness>();
            services.AddTransient<IEmprestimoData, EmprestimoData>();
            services.AddTransient<IEmprestimoAmigoTituloBusiness, EmprestimoAmigoTituloBusiness>();
        }
    }
}
