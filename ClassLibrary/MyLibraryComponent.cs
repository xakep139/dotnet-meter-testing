using System.Diagnostics.Metrics;

namespace ClassLibrary;

public sealed class MyLibraryComponent
{
    public const string MeterName = "myMeterName";

    // As suggested in https://learn.microsoft.com/en-us/dotnet/core/diagnostics/metrics-instrumentation#best-practices
    private static readonly Meter MyMeter =
        new(MeterName);

    private readonly Counter<int> _counter;

    public MyLibraryComponent(string counterName, string units)
    {
        _counter = MyMeter.CreateCounter<int>(counterName, unit: units);
    }

    public void MethodThatEmitsMetrics()
    {
        // Pretend that we're doing something very important here...
        _counter.Add(1);
    }
}