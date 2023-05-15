using System.Diagnostics;
using System.Reflection;
using AspectInjector.Broker;

namespace OtelSample.Common;

[Aspect(Scope.PerInstance)]
public class TracingAspect
{
    [Advice(Kind.Around, Targets = Target.Method | Target.Public)]
    public object Around(
        [Argument(Source.Type)] Type type, 
        [Argument(Source.Name)] string name, 
        [Argument(Source.Arguments)] object[] arguments, 
        [Argument(Source.Target)] Func<object[], object> target)
    {
        var componentName = Assembly.GetCallingAssembly().GetName().Name;
        var className = type.Name;
        
        using var activity = Instrumentation.StartActivity(componentName, $"{componentName}.{className}.{name}");
        
        return target(arguments);
    }
}