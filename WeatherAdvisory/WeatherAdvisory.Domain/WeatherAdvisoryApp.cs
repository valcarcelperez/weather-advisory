using WeatherAdvisory.Domain.Contracts;
using WeatherAdvisory.Domain.Models;

namespace WeatherAdvisory.Domain
{
    public class WeatherAdvisoryApp
    {
        private readonly IWeatherAdvisoryAppLogger _logger;
        private readonly IWeatherAdvisoryService _weatherAdvisoryService;
        private readonly IWeatherService _weatherService;

        public WeatherAdvisoryApp(
            IWeatherAdvisoryAppLogger logger, 
            IWeatherAdvisoryService weatherAdvisoryService,
            IWeatherService weatherService)
        {
            _logger = logger;
            _weatherAdvisoryService = weatherAdvisoryService;
            _weatherService = weatherService;
        }

        public WeatherAdvisoryData GetWeatherAdvisoryData(string zipCode)
        {
            _logger.Log("Running");

            var weatherData = _weatherService.GetWeather(zipCode);

            return new WeatherAdvisoryData
            {
                CanIFlyMyKite = _weatherAdvisoryService.CanIFlyMyKite(weatherData),
                ShouldIGoOutside = _weatherAdvisoryService.ShouldIGoOutside(weatherData),
                ShouldIWearSunscreen = _weatherAdvisoryService.ShouldIWearSunscreen(weatherData)
            };
        }
    }
}
