using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Skeleton_DotNET.Data;
using Skeleton_DotNET.Repositories;
using Skeleton_DotNET.Repositories.Interface;
using Skeleton_DotNET.Services;
using Skeleton_DotNET.Services.Interface;

namespace Skeleton_DotNET.Helpers;

public static class ProgramHelpers
{
    public static void AddJwtAuthentication(this IServiceCollection services, ConfigurationManager config)
    {
        // Cấu hình Authentication
        var key = Encoding.ASCII.GetBytes(config["Jwt:Key"] ?? string.Empty);
        services
            .AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true
                };
            });

        services.AddAuthorization();
    }

    public static void AddCustomMemoryCache(this IServiceCollection services)
    {
        services.AddMemoryCache(options =>
        {
            options.SizeLimit = 1024 * 1024 * 1024; // Kích thước tối đa của cache (1GB)
            options.ExpirationScanFrequency = TimeSpan.FromMinutes(5); // Tần suất quét các mục hết hạn
            options.CompactionPercentage = 0.2; // Tỉ lệ phần trăm nén khi vượt quá giới hạn kích thước
        });
    }

    public static void AddCustomSerilog(this WebApplicationBuilder builder)
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("serilog.json")
            .Build();

        // Cấu hình Serilog
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day) // Ghi log theo ngày
            .CreateLogger();

        builder.Host.UseSerilog(); // Sử dụng Serilog làm logger cho ứng dụng
    }

    public static void AddDbContext(this IServiceCollection services, ConfigurationManager config)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(config.GetConnectionString("DefaultConnection") ?? string.Empty));
    }

    public static void RegisterRepository(this IServiceCollection services)
    {
        services.AddScoped<IWeatherRepository, WeatherRepository>();
    }

    public static void RegisterService(this IServiceCollection services)
    {
        services.AddScoped<IWeatherService, WeatherService>();
    }
}