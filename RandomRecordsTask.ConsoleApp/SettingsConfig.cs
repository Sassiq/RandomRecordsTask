using Microsoft.Extensions.Configuration;

namespace RandomRecordsTask.ConsoleApp
{
    public class SettingsConfig
    {
        private const string file = "appsettings.json";
        public IConfiguration ConfigurationRoot { get; }

        public SettingsConfig()
        {
            this.ConfigurationRoot = new ConfigurationBuilder()
                .SetBasePath(GetAppSettingsPath())
                .AddJsonFile(file)
                .Build();
        }

        private string GetAppSettingsPath()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string? projectDirectory = Directory.GetParent(currentDirectory)?.Parent?.Parent?.FullName;
            if (Directory.GetFiles(currentDirectory).Any(f => Path.GetFileName(f) == file))
            {
                return currentDirectory;
            }
            else if (projectDirectory is not null && Directory.GetFiles(projectDirectory).Any(f => Path.GetFileName(f) == file))
            {
                return projectDirectory;
            }

            throw new ArgumentException("There is no appsettings.json");
        }
    }
}
