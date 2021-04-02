using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text.Json;

namespace WeatherAdvisory.WeatherService.Tests
{
    [TestClass]
    public class WeatherServiceTest
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void GetWeather_Just_for_verification_since_the_returned_values_will_change()
        {
            var config = new WeatherServiceConfig
            {
                BaseAddress = "http://api.weatherstack.com",
                RequestUrl = "current?access_key=610acf4c1d203448cd6f671955c5e8aa&query={zipCode}"
            };

            var target = new WeatherService(config);

            var actual = target.GetWeather("30076");

            Print("Received", actual);
        }

        private void Print(string message, object obj)
        {
            var json = JsonSerializer.Serialize(obj);
            TestContext.WriteLine($"{message}{Environment.NewLine}{json}");
        }
    }
}
