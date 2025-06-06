using SafeQuake.Domain.Entities;

namespace SafeQuake.Application.Interfaces.Earthquake
{
    public interface IUpdateEarthquakeUseCase
    {
        Task ExecuteAsync(EarthquakeEntity earthquake);
    }
} 