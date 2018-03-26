using System.Collections.Generic;
using Projeto.Domain.Entities;

namespace Projeto.Data.Interfaces
{
    public interface IEmprestimoData
    {
        Emprestimo Obter(int codigo);
        List<Emprestimo> ConsultarEmAndamento(string usuario);
        //List<Emprestimo> ConsultarFinalizados(string usuario);
        bool VerificarAmigoPossuiEmprestimo(int codigo);
        bool VerificarTituloPossuiEmprestimo(int codigo);
        void Excluir(int codigo);
        Emprestimo Salvar(Emprestimo emprestimo);
        void Atualizar(Emprestimo emprestimo);
    }
}
