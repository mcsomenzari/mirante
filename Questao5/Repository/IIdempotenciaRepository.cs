namespace Questao5.Repository
{
    public interface IIdempotenciaRepository
    {
        Task CreateIdempotencia(object request, string resultado);
    }
}