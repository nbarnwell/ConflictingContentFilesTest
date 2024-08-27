using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Configuration.Memory;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ConflictingContentFilesTest;

public class TestService(IHost host, IServiceProvider provider, ILogger<TestService> logger, IConfiguration config) : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("CWD:              " + Directory.GetCurrentDirectory());
        var assemblyLocation = Path.GetDirectoryName(GetType().Assembly.Location);
        logger.LogInformation("AssemblyLocation: " + assemblyLocation, "appsettings.json");
        logger.LogInformation("ContentRootPath:  " + Program.ContentRootPath);
        logger.LogInformation("Setting value:    " + config["AAASetting"]);

        var sourcesDetail = new StringBuilder().AppendLine("Config sources:");
        foreach (var source in Program.ConfigSources)
        {
            sourcesDetail.AppendLine(source.GetType().Name + ": " + GetConfigSourceDetail(source));
        }

        logger.LogInformation(sourcesDetail.ToString());

        var assemblyLocationAppSettingsFilePath = Path.Combine(assemblyLocation, "appsettings.json");
        if (File.Exists(assemblyLocationAppSettingsFilePath))
        {
            var configFileContent = await File.ReadAllLinesAsync(assemblyLocationAppSettingsFilePath, cancellationToken);
            logger.LogInformation("AssemblyLocation config content: " + string.Join(Environment.NewLine, configFileContent));
        }
        else
        {
            logger.LogWarning("AppSetttings file does not exist: " + assemblyLocationAppSettingsFilePath);
        }

        var currentDirectoryAppSettingsFilePath = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
        if (File.Exists(currentDirectoryAppSettingsFilePath))
        {
            var configFileContent = await File.ReadAllLinesAsync(currentDirectoryAppSettingsFilePath, cancellationToken);
            logger.LogInformation("CWD config content: " + string.Join(Environment.NewLine, configFileContent));
        }
        else
        {
            logger.LogWarning("AppSetttings file does not exist: " + currentDirectoryAppSettingsFilePath);
        }

        await host.StopAsync(cancellationToken);
    }

    private string GetConfigSourceDetail(IConfigurationSource source)
    {
        return source switch
        {
            JsonConfigurationSource jsonSource => jsonSource.Path,
            FileConfigurationSource fileSource => fileSource.Path,
            MemoryConfigurationSource memorySource => string.Join(", ", memorySource.InitialData.Select(kv => kv.Key + "=" + kv.Value)),
            StreamConfigurationSource streamSource => streamSource.Stream.GetType().Name,
            IConfigurationProvider provider => provider.GetType().Name,
            _ => source.ToString()
        };
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}