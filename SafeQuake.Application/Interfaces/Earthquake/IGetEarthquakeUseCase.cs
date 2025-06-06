using SafeQuake.Domain.Entities;

namespace SafeQuake.Application.Interfaces.Earthquake
{
    public interface IGetEarthquakeUseCase
    {
        Task<EarthquakeEntity?> ExecuteAsync(int id);
    }
} 