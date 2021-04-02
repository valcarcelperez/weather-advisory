using WeatherAdvisory.Domain.Contracts;

namespace WeatherAdvisory.Console
{
    public class ConsoleLocationProvider : ILocationProvider
    {
        public bool TryGetLocation(out string zipcode)
        {
            var attemptCount = 0;
            const int maxAttempts = 5;

            System.Console.WriteLine("Please enter a ZIP code");

            while (true)
            {
                zipcode = System.Console.ReadLine();
                if (ValidateZipCode(zipcode))
                {
                    return true;
                }

                attemptCount++;
                if (attemptCount == maxAttempts)
                {
                    return false;
                }

                System.Console.WriteLine("Please enter a valid ZIP code (5 numbers)");
            }
        }

        private bool ValidateZipCode(string zipcode)
        {
            return zipcode.Length == 5 && int.TryParse(zipcode, out _);
        }
    }
}
