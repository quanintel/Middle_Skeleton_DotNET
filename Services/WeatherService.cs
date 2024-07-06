using AutoMapper;
using Skeleton_DotNET.DTOs;
using Skeleton_DotNET.Repositories.Interface;
using Skeleton_DotNET.Services.Interface;

namespace Skeleton_DotNET.Services;

public class WeatherService : IWeatherService
{
    private readonly IWeatherRepository _weatherRepository;
    private readonly IMapper _mapper;

    public WeatherService(IWeatherRepository weatherRepository, IMapper mapper)
    {
        _weatherRepository = weatherRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<WeatherForecastDto>> GetAsync()
    {
        var forecasts = await _weatherRepository.GetAsync();
        return _mapper.Map<IEnumerable<WeatherForecastDto>>(forecasts);
    }
}