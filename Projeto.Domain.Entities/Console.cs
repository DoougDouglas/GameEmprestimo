using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projeto.Domain.Entities
{
    [Table("CONSOLE")]
    public class Console
    {
        [Column("CD_CONSOLE")]
        [Key]
        public int Codigo { get; set; }

        [Column("DS_CONSOLE")]
        [Required(AllowEmptyStrings = false)]
        public string Descricao { get; set; }
    }
}
