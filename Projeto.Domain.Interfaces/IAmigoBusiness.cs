using System.Collections.Generic;
using Projeto.Domain.Entities;

namespace Projeto.Domain.Interfaces
{
    public interface IAmigoBusiness
    {
        Amigo Obter(int codigo);
        List<Amigo> Consultar(string usuario);
        void Excluir(int codigo);
        void Salvar(Amigo amigo);
    }
}
