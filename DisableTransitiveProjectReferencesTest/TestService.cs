using ExternalLibrary;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DisableTransitiveProjectReferencesTest;

public class TestService(IHost host, IServiceProvider provider, ILogger<TestService> logger, IConfiguration config) : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Starting test...");
        logger.LogInformation("Setting value = " + config["AAASetting"]);
        logger.LogInformation(Class1.Message);

        await host.StopAsync(cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}