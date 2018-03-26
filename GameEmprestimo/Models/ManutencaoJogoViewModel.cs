using System.ComponentModel.DataAnnotations;
using Projeto.Domain.Entities;
using System.Collections.Generic;

namespace GameEmprestimo.Models
{
    public class ManutencaoJogoViewModel
    {
        public int Codigo { get; set; }

        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Display(Name = "Ano de lançamento")]
        public int AnoLancamento { get; set; }

        [Display(Name = "Console")]
        public int CodigoConsole { get; set; }

        public List<Console> Consoles { get; set; }
    }
}