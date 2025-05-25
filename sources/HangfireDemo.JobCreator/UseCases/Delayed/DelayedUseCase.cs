using DustInTheWind.HangfireDemo.JobCreator.Helpers;
using Hangfire;
using MediatR;

namespace DustInTheWind.HangfireDemo.JobCreator.UseCases.Delayed;

internal class DelayedUseCase : IRequestHandler<DelayedRequest>
{
    public Task Handle(DelayedRequest request, CancellationToken cancellationToken)
    {
        TimeSpan delay = request.Delay ?? TimeSpan.FromSeconds(10);

        if (request.QueueNames == null)
        {
            EnqueueJob(request.Message, delay);
        }
        else
        {
            foreach (string queueName in request.QueueNames)
            {
                if (queueName.IsNullOrEmpty())
                    EnqueueJob(request.Message, delay);
                else
                    EnqueueJob(queueName, request.Message, delay);
            }
        }

        return Task.CompletedTask;
    }

    public static void EnqueueJob(string message, TimeSpan delay)
    {
        string fullMessage = $"Hello from delayed job! Created at: {DateTime.UtcNow.ToCustomString()}";

        if (message.IsNotNullOrEmpty())
            fullMessage += $" Message: {message}";

        string jobId = BackgroundJob.Schedule(
            () => Console.WriteLine(fullMessage),
            delay);

        Console.WriteLine($"Delayed job has been created. Id: {jobId}; Queue: [default]; Delay: {delay}");
    }

    public static void EnqueueJob(string queueName, string message, TimeSpan delay)
    {
        string fullMessage = $"Hello from delayed job! Created at: {DateTime.UtcNow.ToCustomString()}";

        if (message.IsNotNullOrEmpty())
            fullMessage += $" Message: {message}";

        string jobId = BackgroundJob.Schedule(
            queueName,
            () => Console.WriteLine(fullMessage),
            delay);

        Console.WriteLine($"Delayed job has been created. Id: {jobId}; Queue: '{queueName}'; Delay: {delay}");
    }
}
