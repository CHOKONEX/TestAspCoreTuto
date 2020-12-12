using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using TestAspCoreTuto.Jobs;
using TestAspCoreTuto.Jobs.SeedWork;

namespace TestAspCoreTuto.Bootstrapping.Extensions
{
    public static class QuartzJobExtension
    {
        public static void AddJobsInjections(this IServiceCollection services)
        {
            // Add Quartz services
            services.AddSingleton<IJobFactory, SingletonJobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();

            // Add our job HelloWorldJob
            services.AddSingleton<HelloWorldJob>();
            services.AddSingleton(new JobSchedule(
                jobType: typeof(HelloWorldJob),
                cronExpression: "0/5 * * * * ?")); // run every 5 seconds

            // Add our job EmailJob
            services.AddSingleton<EmailJob>();
            services.AddSingleton(new JobSchedule(
                jobType: typeof(EmailJob),
                cronExpression: "0/5 * * * * ?")); // run every 5 seconds
        }
    }
}
