using SafeQuake.Application.Interfaces.Earthquake;
using SafeQuake.Domain.Entities;
using SafeQuake.Application.Interfaces.Data;

namespace SafeQuake.Application.UseCases.Earthquake
{
    public class CreateEarthquakeUseCase(IAppDbContext context) : ICreateEarthquakeUseCase
    {
        public async Task ExecuteAsync(EarthquakeEntity earthquake)
        {
            context.Earthquakes.Add(earthquake);
            await context.SaveChangesAsync();
        }
    }
} 