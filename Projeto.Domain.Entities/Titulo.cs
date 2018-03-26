using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projeto.Domain.Entities
{
    [Table("TITULO")]
    public class Titulo
    {
        [Column("CD_TITULO")]
        [Key]
        public int Codigo { get; set; }

        [Column("NM_TITULO")]
        [Required(AllowEmptyStrings = false)]
        public string Nome { get; set; }

        [Column("ANO_LANCAMENTO_TITULO")]
        [Required]
        public short AnoLancamento { get; set; }

        [Column("NM_USUARIO")]
        [Required(AllowEmptyStrings = false)]
        public string Usuario { get; set; }

        [ForeignKey("Console")]
        [Column("CD_CONSOLE")]
        public int ConsoleRefId { get; set; }

        
        [Required]
        public Console Console { get; set; }

        [Column("IS_EMPRESTADO")]
        public string IsEmprestado { get; set; }

        public bool Validar()
        {
            bool valido = true;

            valido &= Nome != null && !string.IsNullOrEmpty(Nome.Trim());
            valido &= AnoLancamento > 0;
            valido &= Console != null && Console.Codigo > 0;

            return valido;
        }
    }
}
