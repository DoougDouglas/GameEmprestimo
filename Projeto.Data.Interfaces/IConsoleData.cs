using System.Collections.Generic;
using Projeto.Domain.Entities;

namespace Projeto.Data.Interfaces
{
    public interface IConsoleData
    {
        List<Console> Listar();
        Console Obter(int codigo);
    }
}
