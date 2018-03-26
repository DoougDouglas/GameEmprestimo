
using System.Data.Entity;

namespace Projeto.Data
{
    public class CrudData<T> where T : class
    {
        public CrudData(ProjetoContext context)
        {
            Context = context;
        }

        protected ProjetoContext Context { get; }

        public virtual T Obter(int codigo)
        {
            return Context.Set<T>().Find(codigo);
        }

        public virtual void Excluir(int codigo)
        {
            Context.Set<T>().Remove(Obter(codigo));
            Context.SaveChanges();
        }

        public virtual T Salvar(T registro)
        {
            Context.Set<T>().Add(registro);
            Context.SaveChanges();
            return registro;
        }

        public virtual void Atualizar(T registro)
        {
            Context.Set<T>().Attach(registro);
            Context.Entry(registro).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
