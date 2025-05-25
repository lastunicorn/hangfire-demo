# Hangfire Demo

Hangfire is a .NET library for creating and running jobs in the background.

https://www.hangfire.io/

## Overview

This repo contains an simple of using the Hangfire library.

Hangfire provides three functionalities:

- create jobs
- run jobs
- monitor jobs

These functionalities can be included all in the same application or each of them in a different application.

This is possible because the jobs, when they are created, are first stored into a database and, at a later time, they are retrieved from that database and executed by the Hangfire server. This architecture allows the server to run in a different process if needed.

## Projects

This demo contains 3 projects, one for each functionality:

- **JobCreator** - creates jobs
- **JobRunner** - runs jobs
- **WebDashboard** - monitors job status

## 1) JobCreator

It is a console application that creates a job each time it is run.

The job is just enqueued and stored in the database. The execution will be performed, later, by the Runner application.

The enqueued job, when it run, writes a message at the Console.

### Usage

There are three types of jobs that can be created:

- Fire-and-forget
- Delayed
- Recurring

**Fire-and-forget**

```cmd
# Create a fire-and-forget job in default queue
DustInTheWind.HangfireDemo.JobCreator.exe fire-and-forget 

# Create a fire-and-forget job in specified queue cotaining custom message
DustInTheWind.HangfireDemo.JobCreator.exe fire-and-forget -q queue-1 -m "This is my custom message"
```

**Delayed**

```cmd
# Create a delayed (default 10 sec) job in default queue
DustInTheWind.HangfireDemo.JobCreator.exe delayed

# Create a delayed (20 sec) job in specified queue cotaining custom message
DustInTheWind.HangfireDemo.JobCreator.exe delayed -q queue-1 -m "This is my custom message for delayed job" -d 00:00:20
```

**Recurring**

```cmd
# Create a recurring job. The id and cron expression are mandatory.
DustInTheWind.HangfireDemo.JobCreator.exe recurring -i "my-job-01" -c "* * * * *"

# Create a recurring job in a custom queue with a custom message.
DustInTheWind.HangfireDemo.JobCreator.exe recurring -i "my-job-02" -c "*/5 * * * *" -q queue-1 -m "My 5 minute recurring job"
```

## 2) JobRunner

This is a console application that runs the enqueued jobs existing in the database.

There are two important steps to keep in mind:

- a) Setup Hangfire
- b) Start Hangfire server

**a) Setup Hangfire**

```c#
string connectionString = "Data Source=localhost; Initial Catalog=HangfireDemo; Integrated Security=true; TrustServerCertificate=True";

GlobalConfiguration.Configuration.UseSqlServerStorage(connectionString);
```

**b) Start Hangfire server**

```c#
BackgroundJobServerOptions options = new()
{
    Queues = ["default", "queue-1"]
};

using (new BackgroundJobServer(options))
{
    Console.WriteLine("Hangfire Server started. Press ENTER to exit...");
    Console.ReadLine();
}
```

**Note**

> To keep the code as simple as possible, the example is written entirely in the `Program.cs` file. In a real application you may want to create a more suitable design.

## 3) WebDashboard

This is a web application that displays the Hengfire Dashboard where the user can check the stratus of the enqueued jobs.

The setup includes two steps:

- a) Configure services
- b) Add the Hangfire Dashboard middleware

**a) Configure services**

```c#
string hangfireConnectionString = "Data Source=localhost; Initial Catalog=HangfireDemo; Integrated Security=true; TrustServerCertificate=True";

builder.Services.AddHangfire(configuration => configuration
    .UseSqlServerStorage(hangfireConnectionString));
```

**b) Add the Hangfire Dashboard middleware**

```c#
app.UseHangfireDashboard();
```

