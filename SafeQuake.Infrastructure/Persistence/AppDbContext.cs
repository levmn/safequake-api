using Microsoft.EntityFrameworkCore;
using SafeQuake.Domain.Entities;

namespace SafeQuake.Infrastructure.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<EarthquakeEntity> EarthquakeEvents { get; set; }

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
