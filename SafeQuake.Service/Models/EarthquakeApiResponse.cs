using System.Text.Json.Serialization;

namespace SafeQuake.Service.Models
{
    public class EarthquakeApiResponse
    {
        [JsonPropertyName("features")]
        public List<EarthquakeFeature> Features { get; set; } = new();
    }

    public class EarthquakeFeature
    {
        [JsonPropertyName("properties")]
        public EarthquakeProperties Properties { get; set; } = new();

        [JsonPropertyName("geometry")]
        public EarthquakeGeometry Geometry { get; set; } = new();
    }

    public class EarthquakeProperties
    {
        [JsonPropertyName("mag")]
        public double Magnitude { get; set; }

        [JsonPropertyName("place")]
        public string Location { get; set; } = string.Empty;

        [JsonPropertyName("time")]
        public long Timestamp { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; } = string.Empty;

        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;
    }

    public class EarthquakeGeometry
    {
        [JsonPropertyName("coordinates")]
        public double[] Coordinates { get; set; } = Array.Empty<double>();
    }
} 