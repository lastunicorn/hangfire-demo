# Hangfire Demo

Hangfire is a .NET library for creating and running jobs in the background.

https://www.hangfire.io/

## Overview

This repo contains a simple example of using the Hengfire library.

Even if all three functionalities (enqueue jobs, execute jobs, monitor jobs) can be included in a single web application, this example contains 3 projects, one for each functionality:

- **JobCreator** - created jobs
- **JobRunner** - runs jobs
- **WebDashboard** - monitor job status

## 1) JobCreator

It is a console application that creates a job each time it is run.

**Usage without arguments:**

```
DustInTheWind.HangfireDemo.JobCreator.exe
```

The job is just enqueued and stored in the database. The execution will be performed, later, by the Runner application.

The enqueued job, when it run, writes a message at the Console.

**Usage with arguments:**

```
DustInTheWind.HangfireDemo.JobCreator.exe queue1 queue2
```

If arguments are provided, each argument is interpreted as a queue name and the application will create a job in each queue.

## 2) JobRunner

It is a console application that runs the enqueued jobs existing in the database.

## 3) WebDashboard

It is a web application that displays the Hengfire dashboard where the user can check the stratus of the enqueued jobs.
