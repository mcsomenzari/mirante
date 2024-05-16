using System.ComponentModel.DataAnnotations;

namespace Questao5.Models
{
    public class ContaCorrente
    {
        [Key]
        [Required]
        public string IdContaCorrente { get; set; }
        public int Numero { get; set; }
        public string Nome { get; set; }
        public int Ativo { get; set; }
        public decimal Saldo { get; internal set; }
    }
}
