using WeatherAdvisory.Domain.Models;

namespace WeatherAdvisory.Console
{
    public class ConsoleWeatherAdvisoryPresenter
    {
        public void Present(WeatherAdvisoryData weatherAdvisoryData)
        {
            PrintQuestion("Should I go outside?", weatherAdvisoryData.ShouldIGoOutside);
            PrintQuestion("Should I wear sunscreen?", weatherAdvisoryData.ShouldIWearSunscreen);
            PrintQuestion("Can I fly my kite?", weatherAdvisoryData.CanIFlyMyKite);
        }

        private static void PrintQuestion(string question, bool response)
        {
            System.Console.WriteLine(question);
            var answer = response ? "Yes" : "No";
            System.Console.WriteLine(answer);
        }
    }
}
