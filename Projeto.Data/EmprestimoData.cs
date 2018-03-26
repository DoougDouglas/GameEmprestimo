using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using Projeto.Data.Interfaces;
using Projeto.Domain.Entities;

namespace Projeto.Data
{
    public class EmprestimoData : CrudData<Emprestimo>, IEmprestimoData
    {
        public EmprestimoData(ProjetoContext context) : base(context)
        {
        }

        public override Emprestimo Obter(int codigo)
        {
            return Context.Emprestimos
                .Include(e => e.Amigo)
                .Include(e => e.Titulo)
                .FirstOrDefault(e => e.Codigo == codigo);
        }

        public List<Emprestimo> ConsultarEmAndamento(string usuario)
        {
            return Context.Emprestimos
                .Include(e => e.Amigo)
                .Include(e => e.Titulo)
                .Where(e => e.Usuario == usuario && e.DataDevolucao == null).ToList();
        }

        //public List<Emprestimo> ConsultarFinalizados(string usuario)
        //{
        //    return Context.Emprestimos
        //        .Include(e => e.Amigo)
        //        .Include(e => e.Titulo)
        //        .Where(e => e.Usuario == usuario && e.DataDevolucao != null).ToList();
        //}

        public bool VerificarAmigoPossuiEmprestimo(int codigo)
        {
            return Context.Emprestimos
                .Include(e => e.Amigo)
                .Any(e => e.Amigo.Codigo == codigo);
        }

        public bool VerificarTituloPossuiEmprestimo(int codigo)
        {
            return Context.Emprestimos
                .Include(e => e.Titulo)
                .Any(e => e.Titulo.Codigo == codigo);
        }
    }
}
