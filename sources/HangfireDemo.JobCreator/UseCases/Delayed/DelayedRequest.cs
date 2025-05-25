using MediatR;

namespace DustInTheWind.HangfireDemo.JobCreator.UseCases.Delayed;

internal class DelayedRequest : IRequest
{
    public List<string> QueueNames { get; set; }
    
    public TimeSpan? Delay { get; set; }
    
    public string Message { get; set; }
}
