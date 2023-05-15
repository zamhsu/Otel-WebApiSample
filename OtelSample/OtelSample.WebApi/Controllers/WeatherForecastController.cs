using Microsoft.AspNetCore.Mvc;
using OtelSample.Common;
using OtelSample.Service;
using OtelSample.Service.Dto;
using OtelSample.WebApi.Models;

namespace OtelSample.WebApi.Controllers;

[Tracing]
[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly IWeatherForecastService _weatherForecastService;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(IWeatherForecastService weatherForecastService,
        IHttpClientFactory httpClientFactory,
        ILogger<WeatherForecastController> logger)
    {
        _weatherForecastService = weatherForecastService;
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }
    
    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<IEnumerable<WeatherForecastOutputModel>> GetAllAsync()
    {
        using var httpClient = _httpClientFactory.CreateClient();
        
        Task getFirst = httpClient.GetAsync("https://test.k6.io");
        Task getSecond = httpClient.GetAsync("https://test.k6.io");
        Task getThird = httpClient.GetAsync("https://test.k6.io");

         await Task.WhenAll(getFirst, getSecond, getThird);
        
        var dto = _weatherForecastService.GetAll();

        _logger.LogWarning("You are getting all data.");

        return Mapper(dto);
    }
    
    private IEnumerable<WeatherForecastOutputModel> Mapper(IEnumerable<WeatherForecastDto> data)
    {
        foreach (var weatherForecast in data)
        {
            var outputModel = new WeatherForecastOutputModel
            {
                Date = weatherForecast.Date,
                Summary = weatherForecast.Summary,
                TemperatureC = weatherForecast.TemperatureC,
                TemperatureF = weatherForecast.TemperatureF
            };

            yield return outputModel;
        }
    }
}