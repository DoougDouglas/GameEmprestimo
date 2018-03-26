using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projeto.Domain.Entities
{
    [Table("AMIGO")]
    public class Amigo
    {
        [Column("CD_AMIGO")]
        [Key]
        public int Codigo { get; set; }

        [Column("NM_AMIGO")]
        [Required(AllowEmptyStrings = false)]
        public string Nome { get; set; }

        [Column("DS_EMAIL")]
        public string Email { get; set; }

        [Column("NR_CELULAR")]
        [Required(AllowEmptyStrings = false)]
        public string Celular { get; set; }

        [Column("NM_USUARIO")]
        [Required(AllowEmptyStrings = false)]
        public string Usuario { get; set; }

        public bool Validar()
        {
            bool valido = true;

            valido &= Nome != null && !string.IsNullOrEmpty(Nome.Trim());
            valido &= Celular != null && !string.IsNullOrEmpty(Celular.Trim());

            return valido;
        }
    }
}
