using System.Diagnostics;
using System.Reflection;

namespace OtelSample.Common;

public static class Instrumentation
{
    private static readonly List<ActivitySource> ActivitySources = new List<ActivitySource>();

    public static Activity StartActivity(string componentName, string activityName)
    {
        var activitySource = GetActivitySource(componentName);

        var activity = activitySource.StartActivity(activityName);
        
        return activity;
    }

    private static ActivitySource GetActivitySource(string activityName)
    {
        var activitySource = ActivitySources.FirstOrDefault(q => q.Name.Contains(activityName));

        if (activitySource is null)
        {
            activitySource = new ActivitySource(activityName);
            ActivitySources.Add(activitySource);
        }

        return activitySource;
    }
}