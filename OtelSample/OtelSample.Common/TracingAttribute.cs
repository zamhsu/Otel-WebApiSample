using AspectInjector.Broker;

namespace OtelSample.Common;

[Injection(typeof(TracingAspect))]
public class TracingAttribute : Attribute
{
    
}