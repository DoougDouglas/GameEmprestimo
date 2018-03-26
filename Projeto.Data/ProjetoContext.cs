using Microsoft.AspNet.Identity.EntityFramework;
using Projeto.Domain.Entities;
using System.Configuration;
using System.Data.Entity;

namespace Projeto.Data
{
    public class ProjetoContext : IdentityDbContext<ApplicationUser>
    {
        public ProjetoContext() : base(ConfigurationManager.ConnectionStrings["DefaultConnection"].Name)
        {
        }

        public virtual DbSet<Amigo> Amigos { get; set; }
        public virtual DbSet<Console> Consoles { get; set; }
        public virtual DbSet<Titulo> Titulos { get; set; }
        public virtual DbSet<Emprestimo> Emprestimos { get; set; }
    }
}
