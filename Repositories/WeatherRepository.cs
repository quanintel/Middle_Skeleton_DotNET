using Microsoft.EntityFrameworkCore;
using Skeleton_DotNET.Data;
using Skeleton_DotNET.Models;
using Skeleton_DotNET.Repositories.Interface;

namespace Skeleton_DotNET.Repositories;

public class WeatherRepository : IWeatherRepository
{
    private readonly ApplicationDbContext _context;

    public WeatherRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<WeatherForecast>> GetAsync()
    {
        return await _context.WeatherForecasts.ToListAsync();
    }
}