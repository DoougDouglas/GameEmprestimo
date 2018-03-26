using System.Collections.Generic;
using Projeto.Domain.Entities;

namespace Projeto.Domain.Interfaces
{
    public interface IEmprestimoBusiness
    {
        Emprestimo Obter(int codigo);
        List<Emprestimo> ConsultarEmAndamento(string usuario);
        //List<Emprestimo> ConsultarFinalizados(string usuario);
        void Excluir(int codigo);
        //void Finalizar(int codigo);
        void Salvar(Emprestimo amigo);
    }
}
