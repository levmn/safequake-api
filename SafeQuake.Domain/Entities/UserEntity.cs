namespace SafeQuake.Domain.Entities;

public class UserEntity
{
    public int Id { get; set; }
    public required String Name { get; set; }
    public required String Email { get; set; }
    public required String Password { get; set; }
    public required String Address { get; set; }
    public List<EarthquakeEntity> EarthquakeEvents { get; set; } = new();
}
