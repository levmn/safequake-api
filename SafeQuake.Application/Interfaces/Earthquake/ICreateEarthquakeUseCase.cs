using SafeQuake.Domain.Entities;

namespace SafeQuake.Application.Interfaces.Earthquake
{
    public interface ICreateEarthquakeUseCase
    {
        Task ExecuteAsync(EarthquakeEntity earthquake);
    }
}