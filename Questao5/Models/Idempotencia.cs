using System.ComponentModel.DataAnnotations;

namespace Questao5.Models
{
    public class Idempotencia
    {
        [Key]
        [Required]
        public string ChaveIdempotencia { get; set; }
        public string Requisicao { get; set; }
        public string Resultado { get; set; }

        public Idempotencia(string requisicao, string resultado)
        {
            ChaveIdempotencia = Guid.NewGuid().ToString();
            Requisicao = requisicao;
            Resultado = resultado;
        }
    }
}
