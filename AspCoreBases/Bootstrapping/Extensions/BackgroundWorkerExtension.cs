using Microsoft.Extensions.DependencyInjection;
using TestAspCoreTuto.BackgroundWorkers;

namespace TestAspCoreTuto.Bootstrapping.Extensions
{
    public static class BackgroundWorkerExtension
    {
        public static void AddHostedServices(this IServiceCollection services)
        {
            services.AddHostedService<Worker>();
            services.AddHostedService<QuartzHostedService>();
        }
    }
}
