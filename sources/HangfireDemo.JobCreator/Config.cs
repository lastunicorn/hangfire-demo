using Microsoft.Extensions.Configuration;

namespace DustInTheWind.HangfireDemo.JobCreator;

internal class Config
{
    private readonly IConfiguration configuration;

    public Config()
    {
        configuration = LoadConfiguration();
    }

    private static IConfiguration LoadConfiguration()
    {
        return new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();
    }

    public string GetConnectionString(string name)
    {
        return configuration.GetConnectionString(name);
    }
}