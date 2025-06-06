﻿using Hangfire;

namespace DustInTheWind.HangfireDemo.JobRunner;

internal class Program
{
    public static void Main(string[] args)
    {
        Config config = new();

        Setup.SetupHangfire(config);

        RunHangfireServer(config);
    }

    private static void RunHangfireServer(Config config)
    {
        BackgroundJobServerOptions options = new()
        {
            Queues = config.GatHengfireQueues()
        };

        using (new BackgroundJobServer(options))
        {
            Console.WriteLine("Hangfire Server started. Press ENTER to exit...");
            Console.ReadLine();
        }
    }
}
