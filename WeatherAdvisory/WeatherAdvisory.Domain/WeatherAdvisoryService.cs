using WeatherAdvisory.Domain.Contracts;
using WeatherAdvisory.Domain.Models;

namespace WeatherAdvisory.Domain
{
    public class WeatherAdvisoryServiceConfig
    {
        public int MaxPrecipitationToGoOutside { get; set; }
        public int MaxUvIndexSafeWithoutSunscreen { get; set; }
        public int MinWindSpeedToFlyAKite { get; set; }
    }

    public class WeatherAdvisoryService : IWeatherAdvisoryService
    {
        private readonly WeatherAdvisoryServiceConfig _config;

        public WeatherAdvisoryService(WeatherAdvisoryServiceConfig config)
        {
            _config = config;
        }

        public bool CanIFlyMyKite(WeatherData weatherData)
        {
            return weatherData.Precipitation == 0 && weatherData.WindSpeed > _config.MinWindSpeedToFlyAKite;
        }

        public bool ShouldIGoOutside(WeatherData weatherData)
        {
            return weatherData.Precipitation <= _config.MaxPrecipitationToGoOutside;
        }

        public bool ShouldIWearSunscreen(WeatherData weatherData)
        {
            return weatherData.UvIndex > _config.MaxUvIndexSafeWithoutSunscreen;
        }
    }
}
