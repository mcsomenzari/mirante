using Questao5.Models;

namespace Questao5.Aplication.Response
{
    public class ContaCorrenteResponse
    {
        public int Numero { get; set; }
        public string Nome { get; set; }
        public string DataConsulta { get; set; }
        public decimal Saldo { get; set; }

        public static explicit operator ContaCorrenteResponse(SaldoContaCorrente contaCorrente)
        {
            return new ContaCorrenteResponse()
            {
                Numero = contaCorrente.Numero,
                Nome = contaCorrente.Nome,
                DataConsulta = DateTime.Now.ToString("dd/MM/yyyy HH:mm"),
                Saldo = contaCorrente.Saldo
            };
        }
    }
}
