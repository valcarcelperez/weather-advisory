namespace WeatherAdvisory.Domain.Contracts
{
    public interface ILocationProvider
    {
        bool TryGetLocation(out string zipcode);
    }
}
