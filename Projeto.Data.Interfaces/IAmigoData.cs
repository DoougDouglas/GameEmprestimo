using System.Collections.Generic;
using Projeto.Domain.Entities;

namespace Projeto.Data.Interfaces
{
    public interface IAmigoData
    {
        Amigo Obter(int codigo);
        List<Amigo> Consultar(string usuario);
        void Excluir(int codigo);
        Amigo Salvar(Amigo amigo);
        void Atualizar(Amigo amigo);
    }
}
