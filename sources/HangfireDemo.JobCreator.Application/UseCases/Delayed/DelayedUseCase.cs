using DustInTheWind.HangfireDemo.JobCreator.Application.Helpers;
using Hangfire;
using MediatR;

namespace DustInTheWind.HangfireDemo.JobCreator.Application.UseCases.Delayed;

internal class DelayedUseCase : IRequestHandler<DelayedRequest>
{
    public Task Handle(DelayedRequest request, CancellationToken cancellationToken)
    {
        string fullMessage = BuildMessage(request.Message);
        TimeSpan delay = request.Delay ?? TimeSpan.FromSeconds(10);

        if (request.QueueNames == null)
        {
            EnqueueJob(fullMessage, delay);
        }
        else
        {
            foreach (string queueName in request.QueueNames)
            {
                if (queueName.IsNullOrEmpty())
                    EnqueueJob(fullMessage, delay);
                else
                    EnqueueJob(queueName, fullMessage, delay);
            }
        }

        return Task.CompletedTask;
    }

    private static string BuildMessage(string message)
    {
        string fullMessage = $"Hello from delayed job! Created at: {DateTime.UtcNow.ToCustomString()}";

        if (message.IsNotNullOrEmpty())
            fullMessage += $" Message: {message}";

        return fullMessage;
    }

    private static void EnqueueJob(string message, TimeSpan delay)
    {
        string jobId = BackgroundJob.Schedule(
            () => Console.WriteLine(message),
            delay);

        Console.WriteLine($"Delayed job has been created. Id: {jobId}; Queue: [default]; Delay: {delay}");
    }

    private static void EnqueueJob(string queueName, string message, TimeSpan delay)
    {
        string jobId = BackgroundJob.Schedule(
            queueName,
            () => Console.WriteLine(message),
            delay);

        Console.WriteLine($"Delayed job has been created. Id: {jobId}; Queue: '{queueName}'; Delay: {delay}");
    }
}
