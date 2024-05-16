using Questao5.Models;

namespace Questao5.Repository
{
    public interface IContaCorrenteRepository
    {
        Task<ContaCorrente> GetById(string idContaCorrente);
        Task<ContaCorrente> GetContaCorrenteAtiva(string idContaCorrente);
        Task<SaldoContaCorrente> GetSaldo(string idContaCorrente);
    }
}