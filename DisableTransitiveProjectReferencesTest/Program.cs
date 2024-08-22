
using DisableTransitiveProjectReferencesTest;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

await Host.CreateDefaultBuilder()
          //.ConfigureAppConfiguration((hostContext, config) =>
          //{
          //    config.AddJsonFile("appsettings.json");
          //})
          .ConfigureServices((hostContext, services) =>
          {
              services.AddHostedService<TestService>();
          })
          .Build()
          .RunAsync();