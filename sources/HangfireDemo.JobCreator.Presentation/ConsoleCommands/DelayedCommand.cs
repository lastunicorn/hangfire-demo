using DustInTheWind.ConsoleTools.Commando;
using DustInTheWind.HangfireDemo.JobCreator.Application.UseCases.Delayed;
using MediatR;

namespace DustInTheWind.HangfireDemo.JobCreator.Presentation.ConsoleCommands;

[NamedCommand("delayed", Description = "Create a delayed job.")]
public class DelayedCommand : IConsoleCommand
{
    private readonly IMediator mediator;

    [NamedParameter("queue", ShortName = 'q', IsOptional = true, Description = "A list of queues in which to create the job.")]
    public List<string> Queues { get; set; }

    [NamedParameter("message", ShortName = 'm', IsOptional = true, Description = "The message to be displayed in the console by the job.")]
    public string Message { get; set; }

    [NamedParameter("delay", ShortName = 'd', IsOptional = true, Description = "The delay after which the job will be executed. Default is 10 seconds.")]
    public TimeSpan? Delay { get; set; }

    public DelayedCommand(IMediator mediator)
    {
        this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    public async Task Execute()
    {
        DelayedRequest request = new()
        {
            QueueNames = Queues,
            Message = Message,
            Delay = Delay
        };

        await mediator.Send(request);
    }
}
