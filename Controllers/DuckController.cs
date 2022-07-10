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
        public ActionResult<Duck> Get(int? id, string? name, string? genus, string? species)
        {
            Duck duck;
            if (id != null)
            {
                duck = _db.Ducks.Single(d => d.Id == id);
            } else if (name != null)
            {
                duck = _db.Ducks.Single(d => d.Name == name);
            } else if (genus != null && species != null)
            {
                duck = _db.Ducks.Single(d => d.Genus == genus);
            } else
            {
                return BadRequest("You need to provide an id, name or genus and species");
            }
            

            _logger.LogTrace($"Getting quackers: {duck.Name} - {duck.Description}");

            return duck;
        }

        [HttpPut(Name = "AddDuck")]
        public ActionResult<Duck> Put(Duck duck)
        {
            _logger.LogTrace($"Going quackers: {duck.Name} - {duck.Description}");

            duck.CreateDate = DateTime.Now;
            duck.LastModified = duck.CreateDate;

            _db.Ducks.Add(duck);

            _db.SaveChanges();

            return duck;
        }

        [HttpPost(Name = "UpdateDuck")]
        public ActionResult<Duck> Post(Duck duck)
        {
            var dbduck = _db.Ducks.Single(d => d.Id == duck.Id);

            dbduck.WingSpanCm = duck.WingSpanCm ?? dbduck.WingSpanCm;
            dbduck.Species = duck.Species ?? dbduck.Species;
            dbduck.Name = duck.Name ?? dbduck.Name;
            dbduck.Description = duck.Description ?? dbduck.Description;
            dbduck.LastModified = DateTime.Now;


            _logger.LogTrace($"Going quackers: {duck.Name} - {duck.Description}");

            _db.SaveChanges();

            return dbduck;
        }

        [HttpDelete(Name = "DeleteDuck")]
        public ActionResult<Duck> Delete(int id)
        {
            var duck = _db.Ducks.Single(d => d.Id == id);

            _logger.LogTrace($"Get the duck outta here: {duck.Name} - {duck.Description}");

            _db.Ducks.Remove(duck);
            _db.SaveChanges();

            return duck;
        }
    }
}