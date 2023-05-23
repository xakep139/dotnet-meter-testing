using OpenTelemetry;
using OpenTelemetry.Metrics;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace TestProject;

public class TestSuiteTwo
{
    [Fact]
    public void TestMemoryMetrics()
    {
        var metricsRead = new List<Metric>();
        using var meterProvider = Sdk.CreateMeterProviderBuilder()
            .AddMeter(ClassLibrary.MyLibraryComponent.MeterName)
            .AddInMemoryExporter(metricsRead, x =>
                x.PeriodicExportingMetricReaderOptions
                    .ExportIntervalMilliseconds = 5)
            .Build();

        var myComponent = new ClassLibrary.MyLibraryComponent("RAM", "GiB");
        for (var i = 0; i < 10000; i++)
        {
            myComponent.MethodThatEmitsMetrics();
        }

        Assert.All(metricsRead.ToList(), x => Assert.Equal("GiB", x.Unit));
    }
}