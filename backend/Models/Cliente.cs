using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class Cliente
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        
        [Column(TypeName = "varchar(70)")]
        [Required]
        public string nome { get; set; }

        public IEnumerable<Operacao> operacoes { get; set; }
        
    }
}