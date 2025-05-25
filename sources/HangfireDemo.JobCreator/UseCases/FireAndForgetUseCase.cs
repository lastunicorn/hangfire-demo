using DustInTheWind.HangfireDemo.JobCreator.Helpers;
using Hangfire;

namespace DustInTheWind.HangfireDemo.JobCreator.UseCases;

internal class FireAndForgetUseCase
{
    public void Execute()
    {
        EnqueueJob("queue-1");
        EnqueueJob("queue-2");
        EnqueueJob();
    }

    public static void EnqueueJob()
    {
        string jobId = BackgroundJob.Enqueue(
            () => Console.WriteLine($"Hello from fire-and-forget job! Created at: {DateTime.UtcNow.ToCustomString()}"));

        Console.WriteLine($"Fire-and-forget job has been created. Id: {jobId}; Queue: [default].");
    }

    public static void EnqueueJob(string queueName)
    {
        string jobId = BackgroundJob.Enqueue(
            queueName,
            () => Console.WriteLine($"Hello from fire-and-forget job! Created at: {DateTime.UtcNow.ToCustomString()}"));

        Console.WriteLine($"Fire-and-forget job has been created. Id: {jobId}; Queue: '{queueName}'.");
    }
}
