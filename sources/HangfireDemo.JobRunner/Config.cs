using Microsoft.Extensions.Configuration;

namespace DustInTheWind.HangfireDemo.JobRunner;

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

    internal string[] GatHengfireQueues()
    {
        IConfigurationSection hangfireSection = configuration.GetSection("Hangfire");

        if (!hangfireSection.Exists())
            return [];

        IConfigurationSection queuesSection = hangfireSection.GetSection("Queues");

        if (!queuesSection.Exists())
            return [];

        return queuesSection.Get<string[]>() ?? [];
    }
}