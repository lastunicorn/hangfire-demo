using DustInTheWind.HangfireDemo.JobCreator.Helpers;
using Hangfire;
using MediatR;

namespace DustInTheWind.HangfireDemo.JobCreator.UseCases.FireAndForget;

internal class FireAndForgetUseCase : IRequestHandler<FireAndForgetRequest>
{
    public Task Handle(FireAndForgetRequest request, CancellationToken cancellationToken)
    {
        if (request.QueueNames == null)
        {
            EnqueueJob(request.Message);
        }
        else
        {
            foreach (string queueName in request.QueueNames)
            {
                if (queueName.IsNullOrEmpty())
                    EnqueueJob(request.Message);
                else
                    EnqueueJob(queueName, request.Message);
            }
        }

        return Task.CompletedTask;
    }

    public static void EnqueueJob(string message)
    {
        string fullMessage = $"Hello from fire-and-forget job! Created at: {DateTime.UtcNow.ToCustomString()}";

        if (message.IsNotNullOrEmpty())
            fullMessage += $" Message: {message}";

        string jobId = BackgroundJob.Enqueue(
            () => Console.WriteLine(fullMessage));

        Console.WriteLine($"Fire-and-forget job has been created. Id: {jobId}; Queue: [default].");
    }

    public static void EnqueueJob(string queueName, string message)
    {
        string fullMessage = $"Hello from fire-and-forget job! Created at: {DateTime.UtcNow.ToCustomString()}";

        if (message.IsNotNullOrEmpty())
            fullMessage += $" Message: {message}";

        string jobId = BackgroundJob.Enqueue(
            queueName,
            () => Console.WriteLine(fullMessage));

        Console.WriteLine($"Fire-and-forget job has been created. Id: {jobId}; Queue: '{queueName}'.");
    }
}
