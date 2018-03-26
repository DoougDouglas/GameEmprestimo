using System.Collections.Generic;
using Projeto.Domain.Entities;

namespace Projeto.Domain.Interfaces
{
    public interface IConsoleBusiness
    {
        List<Console> Listar();
        Console Obter(int codigo);
    }
}
