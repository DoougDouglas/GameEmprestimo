using System.Collections.Generic;
using System.Linq;
using Projeto.Data.Interfaces;
using Projeto.Domain.Entities;

namespace Projeto.Data
{
    public class ConsoleData : IConsoleData
    {
        private readonly ProjetoContext _context;

        public ConsoleData(ProjetoContext context)
        {
            _context = context;
        }

        public List<Console> Listar()
        {
            return _context.Consoles.ToList();
        }

        public Console Obter(int codigo)
        {
            return _context.Consoles.Find(codigo);
        }
    }
}
