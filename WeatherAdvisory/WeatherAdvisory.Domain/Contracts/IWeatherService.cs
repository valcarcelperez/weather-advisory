using WeatherAdvisory.Domain.Models;

namespace WeatherAdvisory.Domain.Contracts
{
    public interface IWeatherService
    {
        WeatherData GetWeather(string zipcode);
    }
}
