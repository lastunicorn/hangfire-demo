using DustInTheWind.ConsoleTools.Commando.Setup.Autofac;
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
                MediatRConfiguration mediatRConfiguration = MediatRConfigurationBuilder.Create(Assembly.GetExecutingAssembly())
                    .WithAllOpenGenericHandlerTypesRegistered()
                    .Build();

                services.RegisterMediatR(mediatRConfiguration);
            })
            .RegisterCommandsFromCurrentAssembly()
            .Build()
            .RunAsync(args);
    }
}
