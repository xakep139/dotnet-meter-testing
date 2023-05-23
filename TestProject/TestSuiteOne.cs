using OpenTelemetry;
using OpenTelemetry.Metrics;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace TestProject;

public class TestSuiteOne
{
    [Fact]
    public void TestProcessorMetrics()
    {
        var metricsRead = new List<Metric>();
        using var meterProvider = Sdk.CreateMeterProviderBuilder()
            .AddMeter(ClassLibrary.MyLibraryComponent.MeterName)
            .AddInMemoryExporter(metricsRead, x =>
                x.PeriodicExportingMetricReaderOptions
                    .ExportIntervalMilliseconds = 5)
            .Build();

        var myComponent = new ClassLibrary.MyLibraryComponent("CPU", "MHz");
        for (var i = 0; i < 10000; i++)
        {
            myComponent.MethodThatEmitsMetrics();
        }

        Assert.All(metricsRead.ToList(), x => Assert.Equal("MHz", x.Unit));
    }
}