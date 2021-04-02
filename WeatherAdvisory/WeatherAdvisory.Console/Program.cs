using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WeatherAdvisory.Domain;
using WeatherAdvisory.Domain.Contracts;

namespace WeatherAdvisory.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder()
                .ConfigureServices(ConfigureServices)
                .Build();

            var consoleWorker = ActivatorUtilities.CreateInstance<WeatherAdvisoryApp>(host.Services);
            consoleWorker.Run();
        }

        private static void ConfigureServices(HostBuilderContext hostContext, IServiceCollection services)
        {
            services.AddSingleton<IWeatherAdvisoryAppLogger>(new ConsoleLogger());
            services.AddSingleton<ILocationProvider>(new ConsoleLocationProvider());
        }
    }
}
