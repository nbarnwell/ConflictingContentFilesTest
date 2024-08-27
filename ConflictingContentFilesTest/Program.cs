
using ConflictingContentFilesTest;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


public static class Program
{
    public static string ContentRootPath;

    public static IList<IConfigurationSource> ConfigSources { get; set; }

    public static async Task Main()
    {
        await Host.CreateDefaultBuilder()
                  .ConfigureAppConfiguration((hostContext, configBuilder) =>
                  {
                      ConfigSources = configBuilder.Sources;
                  })
                  .ConfigureServices((hostContext, services) =>
                  {
                      ContentRootPath = hostContext.HostingEnvironment.ContentRootPath;
                      services.AddHostedService<TestService>();
                  })
                  .Build()
                  .RunAsync();
    }
}
