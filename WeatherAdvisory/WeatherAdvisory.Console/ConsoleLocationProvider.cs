namespace WeatherAdvisory.Console
{
    public class ConsoleLocationProvider 
    {
        public bool TryGetLocation(out string zipCode)
        {
            var attemptCount = 0;
            const int maxAttempts = 5;

            System.Console.WriteLine("Please enter a ZIP code");

            while (true)
            {
                zipCode = System.Console.ReadLine();
                if (ValidateZipCode(zipCode))
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

        private bool ValidateZipCode(string zipCode)
        {
            return zipCode.Length == 5 && int.TryParse(zipCode, out _);
        }
    }
}
