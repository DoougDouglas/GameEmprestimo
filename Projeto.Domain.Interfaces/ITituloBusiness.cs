using System.Collections.Generic;
using Projeto.Domain.Entities;

namespace Projeto.Domain.Interfaces
{
    public interface ITituloBusiness
    {
        Titulo Obter(int codigo);
        List<Titulo> Consultar(string usuario);
        List<Titulo> ConsultarTituloEmprestado(string usuario);
        void Excluir(int codigo);
        void Salvar(Titulo amigo);
    }
}
