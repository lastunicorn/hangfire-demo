using MediatR;

namespace DustInTheWind.HangfireDemo.JobCreator.Application.UseCases.Delayed;

public class DelayedRequest : IRequest
{
    public List<string> QueueNames { get; set; }
    
    public TimeSpan? Delay { get; set; }
    
    public string Message { get; set; }
}
