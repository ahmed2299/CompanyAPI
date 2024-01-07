using Microsoft.AspNetCore.Mvc;

namespace CompanyAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        private readonly List<WeatherForecast> _forecasts=new List<WeatherForecast>();

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        //[HttpPost]
        //public IActionResult PostWeatherForecast([FromBody] WeatherForecast weatherForecast)
        //{
        //    try
        //    {
        //        _forecasts.Add(weatherForecast);
        //        return CreatedAtAction(nameof(Get), new { id = weatherForecast.Date }, weatherForecast);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}

        //[HttpGet]
        //public IActionResult getWeatherForecasts()
        //{
        //    return Ok(_forecasts);
        //}
    }
}
