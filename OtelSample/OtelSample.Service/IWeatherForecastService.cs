using OtelSample.Service.Dto;

namespace OtelSample.Service;

public interface IWeatherForecastService
{
    IEnumerable<WeatherForecastDto> GetAll();
}