using Projeto.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GameEmprestimo.Models
{
    public class EmprestimoViewModel
    {
        public int Codigo { get; set; }

        [Display(Name = "Amigo")]
        public int CodigoAmigo { get; set; }

        [Display(Name = "Jogo")]
        public int CodigoTitulo { get; set; }

        public List<Amigo> Amigos { get; set; }

        public List<Titulo> Titulos { get; set; }
    }
}