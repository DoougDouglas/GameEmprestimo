using System.Collections.Generic;
using Projeto.Domain.Entities;

namespace Projeto.Data.Interfaces
{
    public interface ITituloData
    {
        Titulo Obter(int codigo);
        List<Titulo> Consultar(string usuario);
        List<Titulo> ConsultarTituloEmprestado(string usuario, string isEmprestado);
        void Excluir(int codigo);
        Titulo Salvar(Titulo titulo);
        void Atualizar(Titulo titulo);
    }
}
