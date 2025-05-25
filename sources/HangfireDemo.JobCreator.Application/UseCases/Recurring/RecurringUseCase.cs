using DustInTheWind.HangfireDemo.JobCreator.Application.Helpers;
using Hangfire;
using MediatR;

namespace DustInTheWind.HangfireDemo.JobCreator.Application.UseCases.Recurring;

internal class RecurringUseCase : IRequestHandler<RecurringRequest>
{
    public Task Handle(RecurringRequest request, CancellationToken cancellationToken)
    {
        if (request.JobId.IsNullOrEmpty())
            throw new ArgumentException("Job id cannot be null or empty.", nameof(request.JobId));

        if (request.CronExpression.IsNullOrEmpty())
            throw new ArgumentException("CronExpression expression cannot be null or empty.", nameof(request.CronExpression));

        string fullMessage = BuildMessage(request.Message);

        if (request.QueueNames == null)
        {
            EnqueueJob(request.JobId, fullMessage, request.CronExpression);
        }
        else
        {
            foreach (string queueName in request.QueueNames)
            {
                if (queueName.IsNullOrEmpty())
                    EnqueueJob(request.JobId, fullMessage, request.CronExpression);
                else
                    EnqueueJob(request.JobId, queueName, fullMessage, request.CronExpression);
            }
        }

        return Task.CompletedTask;
    }

    private static string BuildMessage(string message)
    {
        string fullMessage = $"Hello from recurring job! Created at: {DateTime.UtcNow.ToCustomString()}";

        if (message.IsNotNullOrEmpty())
            fullMessage += $" Message: {message}";

        return fullMessage;
    }

    private static void EnqueueJob(string jobId, string message, string cronExpression)
    {
        RecurringJob.AddOrUpdate(
            jobId,
            () => Console.WriteLine(message),
            cronExpression);

        Console.WriteLine($"Recurring job has been created. Id: '{jobId}'; CronExpression: {cronExpression}");
    }

    private static void EnqueueJob(string jobId, string queue, string message, string cronExpression)
    {
        RecurringJob.AddOrUpdate(
            jobId,
            queue,
            () => Console.WriteLine(message),
            cronExpression);

        Console.WriteLine($"Recurring job has been created. Id: '{jobId}'; Cron Expression: {cronExpression}; Queue: {queue}");
    }
}