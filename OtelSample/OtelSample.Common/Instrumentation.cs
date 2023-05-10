using System.Diagnostics;

namespace OtelSample.Common;

public static class Instrumentation
{
    private static readonly string RepositoryActivitySourceName = "OtelSample.Repository";
    private static readonly string ServiceActivitySourceName = "OtelSample.Service";
    private static readonly string WebApiActivitySourceName = "OtelSample.WebApi";

    public static ActivitySource RepositoryActivitySource { get; private set; } = new ActivitySource(RepositoryActivitySourceName);

    public static ActivitySource ServiceActivitySource { get; private set; } = new ActivitySource(ServiceActivitySourceName);
    
    public static ActivitySource WebApiActivitySource { get; private set; } = new ActivitySource(WebApiActivitySourceName);
}