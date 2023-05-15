using System.Diagnostics;
using System.Reflection;

namespace OtelSample.Common;

public static class Instrumentation
{
    private static ActivitySource _activitySource;

    public static Activity StartActivity(string activityName)
    {
        var componentName = Assembly.GetCallingAssembly().GetName().Name;

        var activity = StartActivity(componentName, activityName);

        return activity;
    }
    
    public static Activity StartActivity(string componentName, string activityName)
    {
        if (_activitySource is null)
        {
            _activitySource = new ActivitySource(componentName);
        }

        var activity = _activitySource.StartActivity(activityName);
        
        return activity;
    }
}