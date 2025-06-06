using Microsoft.EntityFrameworkCore;
using SafeQuake.Domain.Entities;

namespace SafeQuake.Application.Interfaces.Data
{
    public interface IAppDbContext
    {
        DbSet<UserEntity> Users { get; set; }
        DbSet<EarthquakeEntity> Earthquakes { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
} 