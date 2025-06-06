namespace SafeQuake.Domain.Entities;

public class EarthquakeEntity
{
    public int Id { get; set; }
    public required Double Latitude { get; set; }
    public required Double Longitude { get; set; }
    public required Double Depth { get; set; }
    public required Double Magnitude { get; set; }
    public required DateTime DateTime { get; set; }
    public required String Location { get; set; }
    public int UserId { get; set; }
    public UserEntity? User { get; set; }
}