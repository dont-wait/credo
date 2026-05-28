using Microsoft.AspNetCore.Mvc;
using CreditScoringAPI.Models;
using CreditScoringAPI.Services;

namespace CreditScoringAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CreditController : ControllerBase
    {
        private readonly PredictionService _service;

        public CreditController(PredictionService service)
        {
            _service = service;
        }

        [HttpPost("predict")]
        public IActionResult Predict(
            [FromBody] CreditRequest request)
        {
            var result = _service.Predict(request);

            return Ok(result);
        }
    }
}