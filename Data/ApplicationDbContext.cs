using Microsoft.EntityFrameworkCore;
using Skeleton_DotNET.Data.Config;
using Skeleton_DotNET.Models;

namespace Skeleton_DotNET.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<WeatherForecast> WeatherForecasts { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new WeatherForecastConfig());
    }
}