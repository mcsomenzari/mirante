using MediatR;
using Questao5.Aplication.Request;
using Questao5.Aplication.Response;
using Questao5.Enum;
using Questao5.Models;
using Questao5.Repository;

namespace Questao5.Application.Handlers
{
    public class MovimentoHandler : IRequestHandler<MovimentoRequest, ApiResult<MovimentoResponse>>
    {
        private readonly IMovimentoRepository _movimentoRepository;
        private readonly IIdempotenciaRepository _idempotenciaRepository;
        private readonly IContaCorrenteRepository _contaCorrenteRepository;

        public MovimentoHandler(IMovimentoRepository movimentoRepository, IIdempotenciaRepository idempotenciaRepository, IContaCorrenteRepository contaCorrenteRepository)
        {
            _movimentoRepository = movimentoRepository;
            _idempotenciaRepository = idempotenciaRepository;
            _contaCorrenteRepository = contaCorrenteRepository;
        }

        public async Task<ApiResult<MovimentoResponse>> Handle(MovimentoRequest request, CancellationToken cancellationToken)
        {
            if (request.ValorMovimentado <= 0)
            {
                var resultado = Erros.INVALID_VALUE.ToString();
                await _idempotenciaRepository.CreateIdempotencia(request, resultado);
                return new ApiResult<MovimentoResponse>(false, 400, dados: null, "O valor movimentado deve ser maior que zero.");
            }

            if (request.TipoMovimento != "C" && request.TipoMovimento != "D")
            {
                var resultado = Erros.INVALID_TYPE.ToString();
                await _idempotenciaRepository.CreateIdempotencia(request, resultado);
                return new ApiResult<MovimentoResponse>(false, 400, dados: null, "Tipo de movimento inválido. Escolha entre D para débito ou C para Crédito.");
            }

            var contaCorrente = await _contaCorrenteRepository.GetById(request.IdContaCorrente);
            if (contaCorrente == null)
            {
                var resultado = Erros.INVALID_ACCOUNT.ToString();
                await _idempotenciaRepository.CreateIdempotencia(request, resultado);
                return new ApiResult<MovimentoResponse>(false, 400, dados: null, "Conta inválida.");
            }

            var contaCorrenteAtiva = await _contaCorrenteRepository.GetContaCorrenteAtiva(request.IdContaCorrente);
            if (contaCorrenteAtiva == null)
            {
                var resultado = Erros.INACTIVE_ACCOUNT.ToString();
                await _idempotenciaRepository.CreateIdempotencia(request, resultado);
                return new ApiResult<MovimentoResponse>(false, 400, dados: null, "Conta inativa.");
            }

            var movimentoId = await _movimentoRepository.CreateMovimento(request.IdContaCorrente, request.TipoMovimento, request.ValorMovimentado);

            return new ApiResult<MovimentoResponse>(true, 201, new MovimentoResponse(movimentoId), null);
        }
    }
}
