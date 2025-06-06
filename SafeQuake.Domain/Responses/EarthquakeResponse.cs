namespace SafeQuake.Domain.Responses
{
    public class EarthquakeResponse
    {
        public required int Id { get; set; }
        public required string Latitude { get; set; }
        public required string Longitude { get; set; }
        public required string Depth { get; set; }
        public required string Magnitude { get; set; }
        public required string DateTime { get; set; }
        public required string Location { get; set; }
        public required int UserId { get; set; }
    }
}
