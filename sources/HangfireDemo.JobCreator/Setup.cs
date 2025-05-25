using Hangfire;

namespace DustInTheWind.HangfireDemo.JobCreator;

internal class Setup
{
    public static void SetupHangfire(Config config)
    {
        string connectionString = config.GetConnectionString("HangfireConnection");
        GlobalConfiguration.Configuration.UseSqlServerStorage(connectionString);
    }
}
