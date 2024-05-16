using MediatR;
using Questao5.Aplication.Request;
using Questao5.Aplication.Response;
using Questao5.Enum;
using Questao5.Models;
using Questao5.Repository;

namespace Questao5.Application.Handlers
{
    public class ContaCorrenteHandler : IRequestHandler<ContaCorrenteRequest, ApiResult<ContaCorrenteResponse>>
    {
        private readonly IIdempotenciaRepository _idempotenciaRepository;
        private readonly IContaCorrenteRepository _contaCorrenteRepository;
        public ContaCorrenteHandler(IContaCorrenteRepository contaCorrenteRepository, IIdempotenciaRepository idempotenciaRepository)
        {
            _contaCorrenteRepository = contaCorrenteRepository;
            _idempotenciaRepository = idempotenciaRepository;
        }

        public async Task<ApiResult<ContaCorrenteResponse>> Handle(ContaCorrenteRequest request, CancellationToken cancellationToken)
        {
            var contaCorrente = await _contaCorrenteRepository.GetById(request.IdContaCorrente);
            if (contaCorrente == null)
            {
                var resultado = Erros.INVALID_ACCOUNT.ToString();
                await _idempotenciaRepository.CreateIdempotencia(request, resultado);
                return new ApiResult<ContaCorrenteResponse>(false, 400, dados: null, "Conta inválida.");
            }

            var contaCorrenteAtiva = await _contaCorrenteRepository.GetContaCorrenteAtiva(request.IdContaCorrente);
            if (contaCorrenteAtiva == null)
            {
                var resultado = Erros.INACTIVE_ACCOUNT.ToString();
                await _idempotenciaRepository.CreateIdempotencia(request, resultado);
                return new ApiResult<ContaCorrenteResponse>(false, 400, dados: null, "Conta inativa.");
            }

            var saldoContaCorrente = await _contaCorrenteRepository.GetSaldo(request.IdContaCorrente);

            var contaCorrenteResponse = new ContaCorrenteResponse()
            {
                Numero = contaCorrente.Numero,
                Nome = contaCorrente.Nome,
                DataConsulta = DateTime.Now.ToString("dd/MM/yyyy HH:mm"),
                Saldo = saldoContaCorrente.Saldo
            };

            return new ApiResult<ContaCorrenteResponse>(true, 200, contaCorrenteResponse, null);
        }
    }
}
