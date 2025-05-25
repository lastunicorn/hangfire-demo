using Hangfire;

namespace DustInTheWind.HangfireDemo.JobRunner;

internal static class Setup
{
    public static void SetupHangfire(Config config)
    {
        string connectionString = config.GetConnectionString("HangfireConnection");
        GlobalConfiguration.Configuration.UseSqlServerStorage(connectionString);
    }
}