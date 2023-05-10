using OtelSample.Repository.Models;

namespace OtelSample.Repository;

public interface IWeatherForecastRepository
{
    IEnumerable<WeatherForecast> GetAll();
}