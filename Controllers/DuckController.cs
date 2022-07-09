using DuckApi.Data;
using Microsoft.AspNetCore.Mvc;

namespace DuckApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DuckController : ControllerBase
    {
        private readonly ILogger<DuckController> _logger;
        private readonly DataContext _db;

        public DuckController(ILogger<DuckController> logger, DataContext db)
        {
            _logger = logger;

            _db = db;

        }

        [HttpGet(Name = "GetDuck")]
        public Duck Get(string kind)
        {
            var duck  = _db.Ducks.Single(d => d.Name == kind);

            _logger.LogTrace($"Getting quackers: {duck.Name} - {duck.Description}");

            return duck;
        }

        [HttpPut(Name = "AddDuck")]
        public Duck Put(Duck duck)
        {
            _logger.LogTrace($"Going quackers: {duck.Name} - {duck.Description}");

            _db.Ducks.Add(duck);

            _db.SaveChanges();

            return duck;
        }
    }
}