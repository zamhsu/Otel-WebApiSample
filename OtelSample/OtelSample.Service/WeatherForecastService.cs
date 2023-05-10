using OtelSample.Common;
using OtelSample.Repository;
using OtelSample.Repository.Models;
using OtelSample.Service.Dto;

namespace OtelSample.Service;

public class WeatherForecastService : IWeatherForecastService
{
    private readonly IWeatherForecastRepository _weatherForecastRepository;

    public WeatherForecastService(IWeatherForecastRepository weatherForecastRepository)
    {
        _weatherForecastRepository = weatherForecastRepository;
    }

    public IEnumerable<WeatherForecastDto> GetAll()
    {
        using var activity = Instrumentation.ServiceActivitySource.StartActivity("OtelSample.Service.WeatherForecastService.GetAll");
        var data = _weatherForecastRepository.GetAll();

        return Mapper(data);
    }

    private IEnumerable<WeatherForecastDto> Mapper(IEnumerable<WeatherForecast> data)
    {
        foreach (var weatherForecast in data)
        {
            var dto = new WeatherForecastDto
            {
                Date = weatherForecast.Date,
                Summary = weatherForecast.Summary,
                TemperatureC = weatherForecast.TemperatureC,
                TemperatureF = weatherForecast.TemperatureF
            };

            yield return dto;
        }
    }
}