using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Questao5.Aplication.Request;

namespace Questao5.Infrastructure.Services.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MovimentoController : BaseController
    {
        private readonly ILogger _logger;
        public MovimentoController(ILogger logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CreateMovimento(MovimentoRequest command)
        {
            try
            {
                var response = await Mediator.Send(command);
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
