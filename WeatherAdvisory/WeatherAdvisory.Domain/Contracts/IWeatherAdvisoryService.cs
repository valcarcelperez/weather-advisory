using WeatherAdvisory.Domain.Models;

namespace WeatherAdvisory.Domain.Contracts
{
    public interface IWeatherAdvisoryService
    {
        bool ShouldIGoOutside(WeatherData weatherData);

        bool ShouldIWearSunscreen(WeatherData weatherData);

        bool CanIFlyMyKite(WeatherData weatherData);
    }
}
