using SafeQuake.Application.Interfaces.Earthquake;
using SafeQuake.Domain.Entities;
using SafeQuake.Application.Interfaces.Data;

namespace SafeQuake.Application.UseCases.Earthquake
{
    public class UpdateEarthquakeUseCase(IAppDbContext context) : IUpdateEarthquakeUseCase
    {
        public async Task ExecuteAsync(EarthquakeEntity earthquake)
        {
            context.Earthquakes.Update(earthquake);
            await context.SaveChangesAsync();
        }
    }
} 