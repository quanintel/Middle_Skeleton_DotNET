using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Skeleton_DotNET.Models;

namespace Skeleton_DotNET.Data.Config;

public class WeatherForecastConfig : IEntityTypeConfiguration<WeatherForecast>
{
    public void Configure(EntityTypeBuilder<WeatherForecast> builder)
    {
        builder.ToTable("WeatherForecast");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.Property(t => t.Date);

        builder.Property(t => t.TemperatureC);

        builder.Property(t => t.Summary).HasMaxLength(255);
    }
}