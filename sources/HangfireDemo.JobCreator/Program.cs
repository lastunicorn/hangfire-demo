using DustInTheWind.ConsoleTools.Commando.Setup.Autofac;
using DustInTheWind.HangfireDemo.JobCreator.Application.UseCases.FireAndForget;
using DustInTheWind.HangfireDemo.JobCreator.Presentation.ConsoleCommands;
using MediatR.Extensions.Autofac.DependencyInjection;
using MediatR.Extensions.Autofac.DependencyInjection.Builder;
using System.Reflection;

namespace DustInTheWind.HangfireDemo.JobCreator;

internal class Program
{
    public static async Task Main(string[] args)
    {
        Config config = new();
        Setup.SetupHangfire(config);

        await ApplicationBuilder.Create()
            .ConfigureServices(services =>
            {
                Assembly useCaseAssembly = typeof(FireAndForgetRequest).Assembly;

                MediatRConfiguration mediatRConfiguration = MediatRConfigurationBuilder.Create(useCaseAssembly)
                    .WithAllOpenGenericHandlerTypesRegistered()
                    .Build();

                services.RegisterMediatR(mediatRConfiguration);
            })
            .RegisterCommandsFromAssemblyContaining(typeof(FireAndForgetCommand))
            .Build()
            .RunAsync(args);
    }
}
