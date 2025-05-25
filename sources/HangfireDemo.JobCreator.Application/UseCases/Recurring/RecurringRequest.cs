using MediatR;

namespace DustInTheWind.HangfireDemo.JobCreator.Application.UseCases.Recurring;

public class RecurringRequest : IRequest
{
    public string JobId { get; set; }

    public List<string> QueueNames { get; set; }

    public string Message { get; set; }

    public string CronExpression { get; set; }
}