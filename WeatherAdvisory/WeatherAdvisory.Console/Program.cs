using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WeatherAdvisory.Domain;
using WeatherAdvisory.Domain.Contracts;
using WeatherAdvisory.WeatherService;

namespace WeatherAdvisory.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder()
                .ConfigureServices(ConfigureServices)
                .Build();

            var locationProvider = new ConsoleLocationProvider();
            if (!locationProvider.TryGetLocation(out string zipCode))
            {
                System.Console.WriteLine("Invalid location. Exiting.");
                return;
            }

            var consoleWorker = ActivatorUtilities.CreateInstance<WeatherAdvisoryApp>(host.Services);
            var weatherAdvisoryData = consoleWorker.GetWeatherAdvisoryData(zipCode);

            var presenter = new ConsoleWeatherAdvisoryPresenter();
            presenter.Present(weatherAdvisoryData);
        }

        private static void ConfigureServices(HostBuilderContext hostContext, IServiceCollection services)
        {
            services.AddSingleton<IWeatherAdvisoryAppLogger>(new ConsoleLogger());

            var weatherAdvisoryServiceConfig = hostContext.Configuration.GetSection(AppConstants.CONFIG_WEATHER_ADVISORY_SERVICE).Get<WeatherAdvisoryServiceConfig>();
            services.AddSingleton<IWeatherAdvisoryService>(new WeatherAdvisoryService(weatherAdvisoryServiceConfig));
            
            var weatherServiceApiClientConfig = hostContext.Configuration.GetSection(AppConstants.CONFIG_WEATHER_SERVICE_API_CLIENT).Get<WeatherServiceApiClientConfig>();
            services.AddSingleton<IWeatherService>(new WeatherServiceApiClient(weatherServiceApiClientConfig));
        }
    }
}
