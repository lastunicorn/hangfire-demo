using DustInTheWind.ConsoleTools.Commando;
using DustInTheWind.HangfireDemo.JobCreator.UseCases.FireAndForget;
using MediatR;

namespace DustInTheWind.HangfireDemo.JobCreator.ConsoleCommands;

[NamedCommand("fire-and-forget")]
internal class FireAndForgetCommand : IConsoleCommand
{
    private readonly IMediator mediator;

    [NamedParameter("queue", ShortName = 'q', IsOptional = true, Description = "A list of queues in which to create the job.")]
    public List<string> Queues { get; set; }

    [NamedParameter("message", ShortName = 'm', IsOptional = true, Description = "The message to be displayed in the console by the job.")]
    public string Message { get; set; }

    public FireAndForgetCommand(IMediator mediator)
    {
        this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    public async Task Execute()
    {
        FireAndForgetRequest request = new()
        {
            QueueNames = Queues,
            Message = Message
        };

        await mediator.Send(request);
    }
}
