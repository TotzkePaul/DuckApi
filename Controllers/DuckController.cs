using Microsoft.AspNetCore.Mvc;

namespace DuckApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DuckController : ControllerBase
    {
        private static readonly string[] Ducks = new[]
        {
            "Mallard", "Bluebill"
        };

        private readonly ILogger<DuckController> _logger;

        public DuckController(ILogger<DuckController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetDuckCall")]
        public string Get()
        {
            _logger.LogTrace("Going quackers");
            return "quack";
        }
    }
}