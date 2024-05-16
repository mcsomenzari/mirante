namespace Questao5.Repository
{
    public interface IMovimentoRepository
    {
        Task<string> CreateMovimento(string idContaCorrente, string tipoMovimento, decimal valor);
    }
}