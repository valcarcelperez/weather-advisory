using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeatherAdvisory.Domain.Models;

namespace WeatherAdvisory.Domain.Tests
{
    [TestClass]
    public class WeatherAdvisoryServiceTest
    {
        private WeatherAdvisoryService _target;

        [TestInitialize]
        public void Initialize()
        {
            var config = new WeatherAdvisoryServiceConfig
            {
                MaxPrecipitationToGoOutside = 0,
                MaxUvIndexSafeWithoutSunscreen = 3,
                MinWindSpeedToFlyAKite = 15
            };

            _target = new WeatherAdvisoryService(config);
        }

        [TestMethod]
        [DataRow(false, 20, true, "when is not raining and wind speed over 15 should return true")]
        [DataRow(false, 15, false, "when is not raining and wind speed is 15 should return false")]
        [DataRow(false, 5, false, "when is not raining and wind speed is below 15 should return true")]
        [DataRow(true, 20, false, "when is raining and wind speed over 15 should return false")]
        [DataRow(true, 15, false, "when is raining and wind speed is 15 should return false")]
        [DataRow(true, 5, false, "when is raining and wind speed is below 15 should return false")]
        public void CanIFlyMyKite_Scenarios(bool isRaining, int windSpeed, bool expected, string description)
        {
            var weatherData = new WeatherData 
            { 
                WindSpeed = windSpeed,
                Precipitation = isRaining ? 1 : 0
            };

            var actual = _target.CanIFlyMyKite(weatherData);

            Assert.AreEqual(expected, actual, description);
        }

        [TestMethod]
        [DataRow(0, true, "when precipitation is zero should return true")]
        [DataRow(1, false, "when precipitation greater than zero should return false")]
        public void ShouldIGoOutside_Scenarios(int precipitation, bool expected, string description)
        {
            var weatherData = new WeatherData { Precipitation = precipitation };

            var actual = _target.ShouldIGoOutside(weatherData);

            Assert.AreEqual(expected, actual, description);
        }
    }
}
