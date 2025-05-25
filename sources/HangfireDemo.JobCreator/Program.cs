using DustInTheWind.HangfireDemo.JobCreator.UseCases;

namespace DustInTheWind.HangfireDemo.JobCreator;

internal class Program
{
    public static void Main(string[] args)
    {
        Config config = new();
        Setup.SetupHangfire(config);

        //FireAndForgetUseCase useCase = new();
        //DelayedUseCase useCase = new();
        RecurringUseCase useCase = new();

        useCase.Execute();
    }
}
