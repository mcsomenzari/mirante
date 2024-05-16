using System.Globalization;

namespace Questao1
{
    public class ContaBancaria {    

        public ContaBancaria(int numeroConta, string nomeTitular)
        {
            NumeroConta = numeroConta;
            NomeTitular = nomeTitular;
        }

        public ContaBancaria(int numeroConta, string nomeTitular, decimal depositoInicial) 
        {
            NumeroConta = numeroConta;
            NomeTitular = nomeTitular;
            Saldo = depositoInicial;
        }

        public int NumeroConta { get; }
        public string NomeTitular { get; set; }
        public decimal Saldo { get; set; } = 0;

        public void Deposito(decimal quantia) 
        { 
            Saldo += quantia;
        } 

        public void Saque(decimal quantia, decimal taxaSaque)
        {
            Saldo -= (quantia + taxaSaque);
        }
        

        public override string ToString()
        {
            return $"Conta {NumeroConta}, Titular: {NomeTitular}, Saldo: $ {Saldo}";
        }

    }
}
