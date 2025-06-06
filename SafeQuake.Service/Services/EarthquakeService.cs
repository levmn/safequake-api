using System.Text.Json;
using SafeQuake.Domain.Entities;
using SafeQuake.Service.Interfaces;
using SafeQuake.Service.Models;

namespace SafeQuake.Service.Services
{
    public class EarthquakeService : IEarthquakeService
    {
        private readonly HttpClient _httpClient;
        private readonly string _usgsApiUrl = "https://earthquake.usgs.gov/fdsnws/event/1/query";
        private readonly string _translationApiUrl = "https://api.mymemory.translated.net/get";

        public EarthquakeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<EarthquakeEntity>> ObterTerremotosRecentesAsync(int limiteDeDias = 30, double magnitudeMinima = 2.5)
        {
            var startDate = DateTime.UtcNow.AddDays(-limiteDeDias).ToString("yyyy-MM-dd");
            var url = $"{_usgsApiUrl}?format=geojson&starttime={startDate}&minmagnitude={magnitudeMinima}";

            try
            {
                var response = await _httpClient.GetStringAsync(url);
                var earthquakeData = JsonSerializer.Deserialize<EarthquakeApiResponse>(response);

                if (earthquakeData?.Features == null)
                    return Enumerable.Empty<EarthquakeEntity>();

                var earthquakes = new List<EarthquakeEntity>();

                foreach (var feature in earthquakeData.Features)
                {
                    var translatedLocation = await TraduzirLocalizacaoAsync(feature.Properties.Location);
                    
                    var earthquake = new EarthquakeEntity
                    {
                        Latitude = feature.Geometry.Coordinates[1],
                        Longitude = feature.Geometry.Coordinates[0],
                        Depth = feature.Geometry.Coordinates[2],
                        Magnitude = feature.Properties.Magnitude,
                        DateTime = DateTimeOffset.FromUnixTimeMilliseconds(feature.Properties.Timestamp).UtcDateTime,
                        Location = translatedLocation
                    };

                    earthquakes.Add(earthquake);
                }

                return earthquakes;
            }
            catch (Exception ex)
            {
                // In a production environment, you should log this error properly
                Console.WriteLine($"Error fetching USGS data: {ex.Message}");
                return Enumerable.Empty<EarthquakeEntity>();
            }
        }

        public async Task<string> TraduzirLocalizacaoAsync(string localizacaoOriginal)
        {
            try
            {
                var url = $"{_translationApiUrl}?q={Uri.EscapeDataString(localizacaoOriginal)}&langpair=en|pt-br";
                var response = await _httpClient.GetStringAsync(url);
                var translationData = JsonSerializer.Deserialize<JsonElement>(response);

                var translation = translationData
                    .GetProperty("responseData")
                    .GetProperty("translatedText")
                    .GetString();

                return translation ?? localizacaoOriginal;
            }
            catch (Exception ex)
            {
                // If translation fails, return the original text
                Console.WriteLine($"Error translating location: {ex.Message}");
                return localizacaoOriginal;
            }
        }
    }
} 