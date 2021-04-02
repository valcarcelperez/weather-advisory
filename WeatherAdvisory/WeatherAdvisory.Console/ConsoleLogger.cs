using WeatherAdvisory.Domain.Contracts;

namespace WeatherAdvisory.Console
{
    public class ConsoleLogger : IWeatherAdvisoryAppLogger
    {
        public void Log(string message) => System.Console.WriteLine($"{nameof(ConsoleLogger)} - {message}");
    }
}
