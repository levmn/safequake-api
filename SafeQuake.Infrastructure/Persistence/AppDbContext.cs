using Microsoft.EntityFrameworkCore;
using SafeQuake.Domain.Entities;
using SafeQuake.Application.Interfaces.Data;

namespace SafeQuake.Infrastructure.Persistence;

public class AppDbContext : DbContext, IAppDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<UserEntity> Users { get; set; }
    public DbSet<EarthquakeEntity> Earthquakes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserEntity>()
            .HasKey(u => u.Id);

        modelBuilder.Entity<EarthquakeEntity>()
            .HasKey(e => e.Id);

        modelBuilder.Entity<UserEntity>()
            .HasMany(u => u.EarthquakeEvents)
            .WithOne(e => e.User)
            .HasForeignKey(e => e.UserId);

        base.OnModelCreating(modelBuilder);
    }
}
