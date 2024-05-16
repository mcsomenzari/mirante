using MediatR;
using Questao5.Aplication.Response;
using Questao5.Models;

namespace Questao5.Aplication.Request
{
    public class ContaCorrenteRequest : IRequest<ApiResult<ContaCorrenteResponse>>
    {
        public ContaCorrenteRequest()
        {

        }
        public ContaCorrenteRequest(string idContaCorrente)
        {
            IdContaCorrente = idContaCorrente;
        }

        public string IdContaCorrente { get; set; }
    }
}
