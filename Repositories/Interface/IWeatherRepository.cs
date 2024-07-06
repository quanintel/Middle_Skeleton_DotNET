using Skeleton_DotNET.Models;

namespace Skeleton_DotNET.Repositories.Interface;

public interface IWeatherRepository
{
    Task<IEnumerable<WeatherForecast>> GetAsync();
}