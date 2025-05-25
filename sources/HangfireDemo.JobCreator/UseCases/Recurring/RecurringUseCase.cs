using DustInTheWind.HangfireDemo.JobCreator.Helpers;
using Hangfire;
using MediatR;

namespace DustInTheWind.HangfireDemo.JobCreator.UseCases.Recurring;

internal class RecurringUseCase : IRequestHandler<RecurringRequest>
{
    public Task Handle(RecurringRequest request, CancellationToken cancellationToken)
    {
        EnqueuedJob(request.JobId, request.Cron);

        return Task.CompletedTask;
    }

    private static void EnqueuedJob(string jobId, string cron)
    {
        RecurringJob.AddOrUpdate(
            jobId,
            () => Console.WriteLine($"Hello from recurring job! Created at: {DateTime.UtcNow.ToCustomString()}"),
            cron);

        Console.WriteLine($"Recurring job has been created. Id: '{jobId}'");
    }
}