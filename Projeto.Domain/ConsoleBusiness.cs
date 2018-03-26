using System.Collections.Generic;
using Projeto.Data.Interfaces;
using Projeto.Domain.Entities;
using Projeto.Domain.Interfaces;

namespace Projeto.Domain
{
    public class ConsoleBusiness : IConsoleBusiness
    {
        private readonly IConsoleData _consoleData;

        public ConsoleBusiness(IConsoleData consoleData)
        {
            _consoleData = consoleData;
        }

        public List<Console> Listar()
        {
            return _consoleData.Listar();
        }

        public Console Obter(int codigo)
        {
            return _consoleData.Obter(codigo);
        }
    }
}
