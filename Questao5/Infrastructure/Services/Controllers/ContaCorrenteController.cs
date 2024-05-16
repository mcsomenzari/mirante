using Microsoft.AspNetCore.Mvc;
using Questao5.Aplication.Request;
using Questao5.Aplication.Response;

namespace Questao5.Infrastructure.Services.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ContaCorrenteController : BaseController
    {
        private readonly ILogger _logger;
        public ContaCorrenteController(ILogger logger)
        {
            _logger = logger;
        }

        [HttpGet("{idContaCorrente}")]
        public async Task<ActionResult<ContaCorrenteResponse>> GetContaCorrenteSaldo([FromRoute] string idContaCorrente)
        {
            try
            {
                var response = await Mediator.Send(new ContaCorrenteRequest(idContaCorrente));
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
