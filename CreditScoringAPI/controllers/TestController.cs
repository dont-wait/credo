using CreditScoringAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CreditScoringAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ICreditService _creditService;

        public TestController(ICreditService creditService)
        {
            _creditService = creditService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_creditService.Test());
        }
    }
}