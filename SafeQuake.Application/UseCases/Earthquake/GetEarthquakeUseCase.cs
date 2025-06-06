using Microsoft.EntityFrameworkCore;
using SafeQuake.Application.Interfaces.Earthquake;
using SafeQuake.Domain.Entities;
using SafeQuake.Application.Interfaces.Data;

namespace SafeQuake.Application.UseCases.Earthquake
{
    public class GetEarthquakeUseCase(IAppDbContext context) : IGetEarthquakeUseCase
    {
        public async Task<EarthquakeEntity?> ExecuteAsync(int id)
        {
            return await context.Earthquakes
                .Include(e => e.User)
                .FirstOrDefaultAsync(e => e.Id == id);
        }
    }
} 