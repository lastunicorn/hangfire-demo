using DustInTheWind.HangfireDemo.JobCreator.Helpers;
using Hangfire;
using MediatR;

namespace DustInTheWind.HangfireDemo.JobCreator.UseCases.FireAndForget;

internal class FireAndForgetUseCase : IRequestHandler<FireAndForgetRequest>
{
    public Task Handle(FireAndForgetRequest request, CancellationToken cancellationToken)
    {
        string fullMessage = BuildMessage(request.Message);

        if (request.QueueNames == null)
        {
            EnqueueJob(fullMessage);
        }
        else
        {
            foreach (string queueName in request.QueueNames)
            {
                if (queueName.IsNullOrEmpty())
                    EnqueueJob(fullMessage);
                else
                    EnqueueJob(queueName, fullMessage);
            }
        }

        return Task.CompletedTask;
    }

    private static string BuildMessage(string message)
    {
        string fullMessage = $"Hello from fire-and-forget job! Created at: {DateTime.UtcNow.ToCustomString()}";

        if (message.IsNotNullOrEmpty())
            fullMessage += $" Message: {message}";

        return fullMessage;
    }

    public static void EnqueueJob(string message)
    {
        string jobId = BackgroundJob.Enqueue(
            () => Console.WriteLine(message));

        Console.WriteLine($"Fire-and-forget job has been created. Id: {jobId}; Queue: [default].");
    }

    public static void EnqueueJob(string queueName, string message)
    {
        string jobId = BackgroundJob.Enqueue(
            queueName,
            () => Console.WriteLine(message));

        Console.WriteLine($"Fire-and-forget job has been created. Id: {jobId}; Queue: '{queueName}'.");
    }
}
