using System.ComponentModel.DataAnnotations;

namespace GameEmprestimo.Models
{
    public class AmigoViewModel
    {
        public int Codigo { get; set; }

        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Display(Name = "Celular")]
        public string Celular { get; set; }
    }
}