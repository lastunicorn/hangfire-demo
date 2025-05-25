using MediatR;

namespace DustInTheWind.HangfireDemo.JobCreator.UseCases.FireAndForget;

internal class FireAndForgetRequest : IRequest
{
    public List<string> QueueNames { get; set; }
    
    public string Message { get; set; }
}