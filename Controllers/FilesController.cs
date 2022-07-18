using CsvHelper;
using CsvHelper.Configuration;
using DuckApi.Data;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using Microsoft.AspNetCore.Authorization;

namespace DuckApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class FilesController : ControllerBase
    {
        private readonly ILogger<DucksController> _logger;
        private readonly DataContext _db;

        public FilesController(ILogger<DucksController> logger, DataContext db)
        {
            _logger = logger;
            _db = db;
        }

        [HttpPost]
        public IActionResult UploadCSV(IFormFile file)
        {
            _logger.LogInformation(file.FileName);
            var fileExtension = Path.GetExtension(file.FileName);
            if(fileExtension != ".csv")
            {
                return BadRequest($"Non csv extension. Uploaded extension: {fileExtension}");
            }


            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HeaderValidated = null,
                MissingFieldFound = null
            };

            using (var reader = new StreamReader(file.OpenReadStream()))
            using (var csvReader = new CsvReader(reader, config))
            {

                var ducksToUpload = csvReader.GetRecords<Duck>().ToList();
                var uploadTime = DateTime.Now;

                ducksToUpload.ForEach(d =>
                {
                    d.CreateDate = uploadTime;
                    d.LastModified = uploadTime;
                });

                _db.AddRange(ducksToUpload);

                _db.SaveChanges();
            }

            return Ok("Success");
        }
    }
}
