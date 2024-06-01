using System.Reflection;
using OpenTelemetry.Exporter;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using OtelSample.Repository;
using OtelSample.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient();

builder.Services.AddScoped<IWeatherForecastRepository, WeatherForecastRepository>();
builder.Services.AddScoped<IWeatherForecastService, WeatherForecastService>();

// 反射取得服務相關的類別庫名稱
var serviceName = Assembly.GetEntryAssembly()?.GetName().Name;
var serviceVersion = Assembly.GetEntryAssembly()?.GetName().Version?.ToString();

var componentPrefix = "OtelSample";

var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(q => q.GetName().Name.StartsWith(componentPrefix));
var sources = assemblies.Select(q => q.GetName().Name);

// tracing
builder.Services.AddOpenTelemetry()
    .WithTracing(tracerProviderBuilder =>
        tracerProviderBuilder
            .SetResourceBuilder(
                ResourceBuilder.CreateDefault()
                    .AddService(serviceName, serviceVersion: serviceVersion))
            .AddSource(sources.ToArray())
            .AddAspNetCoreInstrumentation(options =>
            {
                options.RecordException = true;
            })
            .AddHttpClientInstrumentation(options =>
            {
                options.RecordException = true;
            })
            .AddSqlClientInstrumentation(options =>
            {
                options.RecordException = true;
                options.SetDbStatementForText = true;
            })
            .AddOtlpExporter(cfg =>
            {
                cfg.Endpoint = new Uri("http://localhost:4317");
                cfg.Protocol = OtlpExportProtocol.Grpc;
            })
            .AddConsoleExporter());

builder.Services.AddOpenTelemetry()
    .WithMetrics(meterProvider => meterProvider
        .AddAspNetCoreInstrumentation()
        .AddHttpClientInstrumentation()
        .AddRuntimeInstrumentation()
        .AddPrometheusExporter()
        .AddOtlpExporter(cfg =>
        {
            cfg.Endpoint = new Uri("http://localhost:4317");
            cfg.Protocol = OtlpExportProtocol.Grpc;
        })
        .AddConsoleExporter());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();