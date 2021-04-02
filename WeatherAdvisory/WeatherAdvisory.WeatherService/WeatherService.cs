using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using WeatherAdvisory.Domain.Contracts;
using WeatherAdvisory.Domain.Models;

namespace WeatherAdvisory.WeatherService
{
    public class WeatherServiceConfig
    {
        public string BaseAddress { get; set; }
        public string RequestUrl { get; set; }
    }

    public class WeatherService : IWeatherService, IDisposable
    {
        private readonly HttpClient _client;
        private readonly WeatherServiceConfig _config;
        private bool _disposed = false;

        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public WeatherService(WeatherServiceConfig config)
        {
            _config = config;
            _client = new HttpClient
            {
                BaseAddress = new Uri(config.BaseAddress)
            };

            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        ~WeatherService()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _client.Dispose();
                }

                _disposed = true;
            }
        }

        public WeatherData GetWeather(string zipCode)
        {
            var requestUrl = _config.RequestUrl.Replace("{zipCode}", zipCode);
            var request = new HttpRequestMessage(HttpMethod.Post, requestUrl);
            
            var task = Task.Run(async () => 
            {
                var response = await _client.SendAsync(request);
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<WeatherApiDataResponse>(content, _jsonSerializerOptions);
            });

            task.Wait();
            var response = task.Result;

            if (response.Current == null)
            {
                throw new Exception("Invalid data received");
            }

            return MappToDomain(response.Current);
        }

        private static WeatherData MappToDomain(WeatherApiDataCurrent weatherApiDataCurrent)
        {
            return new WeatherData
            {
                Precipitation = weatherApiDataCurrent.Precip,
                UvIndex = weatherApiDataCurrent.Uv_Index,
                WindSpeed = weatherApiDataCurrent.Wind_Speed
            };
        }

        private class WeatherApiDataResponse
        {
            public WeatherApiDataCurrent Current { get; set; }
        }

        private class WeatherApiDataCurrent
        {
            public int Precip { get; set; }
            public int Uv_Index { get; set; }
            public int Wind_Speed { get; set; }
        }
    }
}
