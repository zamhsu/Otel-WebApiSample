using OtelSample.Common;
using OtelSample.Repository.Models;

namespace OtelSample.Repository;

public class WeatherForecastRepository : IWeatherForecastRepository
{
    public IEnumerable<WeatherForecast> GetAll()
    {
        using var activity = Instrumentation.RepositoryActivitySource.StartActivity("OtelSample.Repository.WeatherForecastRepository.GetAll");

        return GetFakeData();
    }

    private IEnumerable<WeatherForecast> GetFakeData()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        });
    }
    
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };
}