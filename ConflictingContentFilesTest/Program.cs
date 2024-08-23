
using ConflictingContentFilesTest;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

await Host.CreateDefaultBuilder()
          .ConfigureServices((hostContext, services) =>
          {
              services.AddHostedService<TestService>();
          })
          .Build()
          .RunAsync();