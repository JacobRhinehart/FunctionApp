using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Functions.Worker;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetry();

    })
    .ConfigureLogging(logging =>
    {
        logging.AddConsole();
        logging.AddApplicationInsights();

        //log everything and the kitchen sink
        logging.SetMinimumLevel(LogLevel.Trace);
    })
    .Build();

await host.RunAsync();