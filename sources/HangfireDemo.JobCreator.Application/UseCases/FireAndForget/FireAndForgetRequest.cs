using MediatR;

namespace DustInTheWind.HangfireDemo.JobCreator.Application.UseCases.FireAndForget;

public class FireAndForgetRequest : IRequest
{
    public List<string> QueueNames { get; set; }
    
    public string Message { get; set; }
}