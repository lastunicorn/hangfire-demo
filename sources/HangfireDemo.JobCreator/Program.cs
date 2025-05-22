using DustInTheWind.HangfireDemo.JobCreator.Helpers;
using Hangfire;

namespace DustInTheWind.HangfireDemo.JobCreator;

internal class Program
{
    public static void Main(string[] args)
    {
        Config config = new();

        SetupHangfire(config);
        CreateHangfireJob(args);
    }

    private static void SetupHangfire(Config config)
    {
        string connectionString = config.GetConnectionString("HangfireConnection");
        GlobalConfiguration.Configuration.UseSqlServerStorage(connectionString);
    }

    private static void CreateHangfireJob(string[] queueNames)
    {
        if (queueNames == null || queueNames.Length == 0)
            queueNames = ["default"];

        IEnumerable<string> queueNamesNotEmpty = queueNames.Where(x => x.IsNotNullOrEmpty());

        foreach (string queueName in queueNamesNotEmpty)
        {
            string jobId = BackgroundJob.Enqueue(queueName, () => Console.WriteLine($"Hello world! Created at: {DateTime.UtcNow.ToFullString()}"));
            Console.WriteLine($"Job {jobId} has been created in queue '{queueName}'.");
        }
    }
}