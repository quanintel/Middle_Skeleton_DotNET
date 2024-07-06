using Skeleton_DotNET.DTOs;

namespace Skeleton_DotNET.Services.Interface;

public interface IWeatherService
{
    Task<IEnumerable<WeatherForecastDto>> GetAsync();
}