using DustInTheWind.HangfireDemo.JobCreator.Helpers;
using Hangfire;

namespace DustInTheWind.HangfireDemo.JobCreator.UseCases;

internal class DelayedUseCase
{
    public void Execute()
    {
        EnqueueJob("queue-1", TimeSpan.FromSeconds(10));
        EnqueueJob("queue-2", TimeSpan.FromSeconds(20));
        EnqueueJob(TimeSpan.FromSeconds(30));
    }

    public static void EnqueueJob(TimeSpan delay)
    {
        string jobId = BackgroundJob.Schedule(
            () => Console.WriteLine($"Hello from delayed job! Created at: {DateTime.UtcNow.ToCustomString()}"),
            delay);

        Console.WriteLine($"Delayed job has been created. Id: {jobId}; Queue: [default]; Delay: {delay}");
    }

    public static void EnqueueJob(string queueName, TimeSpan delay)
    {
        string jobId = BackgroundJob.Schedule(
            queueName,
            () => Console.WriteLine($"Hello from delayed job! Created at: {DateTime.UtcNow.ToCustomString()}"),
            delay);

        Console.WriteLine($"Delayed job has been created. Id: {jobId}; Queue: '{queueName}'; Delay: {delay}");
    }
}