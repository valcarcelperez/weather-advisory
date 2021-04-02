using WeatherAdvisory.Domain.Contracts;

namespace WeatherAdvisory.Domain
{
    public class WeatherAdvisoryApp
    {
        private readonly IWeatherAdvisoryAppLogger _logger;

        public WeatherAdvisoryApp(IWeatherAdvisoryAppLogger logger)
        {
            _logger = logger;
        }

        public void Run()
        {
            _logger.Log("Running");
        }
    }
}
