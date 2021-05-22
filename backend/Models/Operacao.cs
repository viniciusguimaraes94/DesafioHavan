using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class Operacao
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required]
        [Column (TypeName = "Decimal(15, 2)")]
        public decimal valorOriginal { get; set; }

        [Required]
        [Column (TypeName = "Decimal(15, 2)")]
        public decimal valorConvertido { get; set; }

        [Required]
        [Column (TypeName = "Decimal(15, 2)")]
        public decimal taxa { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime dataOperacao { get; set; }

        [ForeignKey("Moeda")]
        public int moedaOrigemId { get; set; }

        [ForeignKey("Moeda")]
        public int moedaDestinoId { get; set; } 

        public Moeda moedaOrigem { get; set; }

        public Moeda moedaDestino { get; set; }


        [ForeignKey("cliente")]
        public int clienteId { get; set; }

        public Cliente cliente { get; set; }
        
    }
}