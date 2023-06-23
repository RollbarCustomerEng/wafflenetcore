using Microsoft.AspNetCore.Mvc;
using Rollbar;
using Rollbar.AppSettings;


namespace wafflenetcore.Controllers
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

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;

            RollbarLocator.RollbarInstance.Configure(new RollbarLoggerConfig("7b340a9146d04bd2be796a7f9fe9443e", "development") {  });
            RollbarLocator.RollbarInstance.Info("NET Core Controller Loaded");
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            int x = 0;
            int y = -1;

            try {

                return Enumerable.Range(x, y).Select(index => new WeatherForecast
                {
                    Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                }).ToArray();

            } catch (Exception ex) {
                RollbarLocator.RollbarInstance.Critical(ex);

                return Enumerable.Empty<WeatherForecast>();
            }


        }
    }
}