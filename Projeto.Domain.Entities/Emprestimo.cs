using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projeto.Domain.Entities
{
    [Table("EMPRESTIMO")]
    public class Emprestimo
    {
        [Column("CD_EMPRESTIMO")]
        [Key]
        public int Codigo { get; set; }

        [ForeignKey("Amigo")]
        [Column("CD_AMIGO")]
        public int AmigoRefId { get; set; }

        [Required]
        public Amigo Amigo { get; set; }

        [ForeignKey("Titulo")]
        [Column("CD_TITULO")]
        public int TituloRefId { get; set; }

        [Required]
        public Titulo Titulo { get; set; }

        [Column("DT_EMPRESTIMO")]
        [Required]
        public DateTime DataEmprestimo { get; set; }

        [Column("DT_DEVOLUCAO_EMPRESTIMO")]
        public DateTime? DataDevolucao { get; set; }

        [Column("NM_USUARIO")]
        [Required(AllowEmptyStrings = false)]
        public string Usuario { get; set; }

        public bool Validar()
        {
            bool valido = true;

            valido &= Amigo != null && Amigo.Codigo > 0;
            valido &= Titulo != null && Titulo.Codigo > 0;

            return valido;
        }
    }
}
