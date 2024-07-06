using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Skeleton_DotNET.DTOs;
using Skeleton_DotNET.Models;
using Skeleton_DotNET.Services.Interface;

namespace Skeleton_DotNET.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IWeatherService _weatherService;
    private readonly IMemoryCache _memoryCache;

    public WeatherForecastController(IWeatherService weatherService, IMemoryCache memoryCache, ILogger<WeatherForecastController> logger)
    {
        _weatherService = weatherService;
        _memoryCache = memoryCache;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetWeatherForecasts()
    {
        var forecasts = await _weatherService.GetAsync();
        return Ok(forecasts);
    }
    
    [HttpGet]
    [Route("GetByCache")]
    public async Task<IActionResult> Get()
    {
        const string cacheKey = "WeatherData";
        if (_memoryCache.TryGetValue(cacheKey, out IEnumerable<WeatherForecastDto> weatherData))
            return Ok(weatherData);
        
        // Nếu không tìm thấy trong cache, hãy tạo dữ liệu mới
        weatherData = await _weatherService.GetAsync();

        // Thiết lập các tùy chọn cache
        var cacheEntryOptions = new MemoryCacheEntryOptions()
            .SetSlidingExpiration(TimeSpan.FromMinutes(5)) // Thời gian hết hạn cache
            .SetSize(1); // Kích thước của cache entry
        
        // Lưu trữ dữ liệu trong cache
        _memoryCache.Set(cacheKey, weatherData, cacheEntryOptions);

        return Ok(weatherData);
    }
}
