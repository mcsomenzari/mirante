using MediatR;
using Questao5.Aplication.Response;
using Questao5.Models;

namespace Questao5.Aplication.Request
{
    public class MovimentoRequest : IRequest<ApiResult<MovimentoResponse>>
    {
        public string IdContaCorrente { get; set; }
        public decimal ValorMovimentado { get; set; }
        public string TipoMovimento { get; set; }

    }
}
