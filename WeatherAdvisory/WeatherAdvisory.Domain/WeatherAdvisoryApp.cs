using WeatherAdvisory.Domain.Contracts;

namespace WeatherAdvisory.Domain
{
    public class WeatherAdvisoryApp
    {
        private readonly IWeatherAdvisoryAppLogger _logger;
        private readonly ILocationProvider _locationProvider;

        public WeatherAdvisoryApp(IWeatherAdvisoryAppLogger logger, ILocationProvider locationProvider)
        {
            _logger = logger;
            _locationProvider = locationProvider;
        }

        public void Run()
        {
            _logger.Log("Running");

            if (!_locationProvider.TryGetLocation(out string zipCode))
            {
                _logger.Log("Invalid location. Exiting.");
                return;
            }

            _logger.Log($"Location '{zipCode}' entered.");
        }
    }
}
