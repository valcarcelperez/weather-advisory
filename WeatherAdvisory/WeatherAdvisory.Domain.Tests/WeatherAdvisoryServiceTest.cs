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
        public void CanIFlyMyKite_Given_wind_speed_is_over_15_Should_return_true()
        {
            var weatherData = new WeatherData { WindSpeed = 20 };
            
            var actual = _target.CanIFlyMyKite(weatherData);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void CanIFlyMyKite_Given_wind_speed_is_15_Should_return_false()
        {
            var weatherData = new WeatherData { WindSpeed = 15 };

            var actual = _target.CanIFlyMyKite(weatherData);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void CanIFlyMyKite_Given_wind_speed_is_below_15_Should_return_false()
        {
            var weatherData = new WeatherData { WindSpeed = 5 };

            var actual = _target.CanIFlyMyKite(weatherData);

            Assert.IsFalse(actual);
        }

        // Using DataRow
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
