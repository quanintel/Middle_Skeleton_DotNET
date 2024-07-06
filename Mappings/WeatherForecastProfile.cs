using AutoMapper;
using Skeleton_DotNET.DTOs;
using Skeleton_DotNET.Models;

namespace Skeleton_DotNET.Mappings;

public class WeatherForecastProfile : Profile
{
    public WeatherForecastProfile()
    {
        CreateMap<WeatherForecast, WeatherForecastDto>();
        CreateMap<WeatherForecastDto, WeatherForecast>();
    }
}