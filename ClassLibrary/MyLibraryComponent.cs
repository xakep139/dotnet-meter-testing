using System.Diagnostics.Metrics;

namespace ClassLibrary;

public sealed class MyLibraryComponent
{
    public const string MeterName = "myMeterName";

    // As suggested in https://learn.microsoft.com/en-us/dotnet/core/diagnostics/metrics-instrumentation#best-practices
    private static readonly Meter MyMeter =
        new(MeterName);

    public void MethodThatEmitsMetrics(
        string counterName,
        string units)
    {
        var counter = MyMeter.CreateCounter<int>(
            counterName,
            unit: units);

        counter.Add(1);
    }
}