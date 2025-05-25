using DustInTheWind.ConsoleTools.Commando;
using DustInTheWind.HangfireDemo.JobCreator.UseCases.Recurring;
using MediatR;

namespace DustInTheWind.HangfireDemo.JobCreator.ConsoleCommands;

[NamedCommand("recurring", Description = "Creates a recurring job.")]
internal class RecurringCommand : IConsoleCommand
{
    private readonly IMediator mediator;

    [NamedParameter("id", ShortName = 'i', Description = "The id of the recurring job to be created.")]
    public string Id { get; set; }

    [NamedParameter("queue", ShortName = 'q', IsOptional = true, Description = "A list of queues in which to create the job.")]
    public List<string> Queues { get; set; }

    [NamedParameter("message", ShortName = 'm', IsOptional = true, Description = "The message to be displayed in the console by the job.")]
    public string Message { get; set; }

    [NamedParameter("cron", ShortName = 'c', Description = "The cron expression that defines the schedule for the recurring job.")]
    public string Cron { get; set; }

    public RecurringCommand(IMediator mediator)
    {
        this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    public async Task Execute()
    {
        RecurringRequest request = new()
        {
            JobId = Id,
            QueueNames = Queues,
            Message = Message,
            CronExpression = Cron
        };

        await mediator.Send(request);
    }
}
