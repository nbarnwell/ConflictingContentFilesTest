namespace ConflictingContentFilesTest;

public static class Program
{
    public static async Task Main()
    {
        var assemblyLocationAppSettingsFilePath = Path.GetFullPath("appsettings.json");
        if (File.Exists(assemblyLocationAppSettingsFilePath))
        {
            var configFileContent = await File.ReadAllLinesAsync(assemblyLocationAppSettingsFilePath);
            Console.WriteLine(assemblyLocationAppSettingsFilePath + " content:");
            Console.WriteLine(string.Join(Environment.NewLine, configFileContent));
        }
        else
        {
            Console.WriteLine("AppSetttings file does not exist");
        }
    }
}