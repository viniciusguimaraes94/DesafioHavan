

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class Moeda
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        
        [Column(TypeName = "varchar(70)")]
        [Required]
        public string descricao { get; set; }

        [Required]
        [Column (TypeName = "Decimal(15, 2)")]
        public decimal valor { get; set; }

        public IEnumerable<Operacao> operacoesOrigem { get; set; }
        public IEnumerable<Operacao> operacoesDestino { get; set; }
    }
}
