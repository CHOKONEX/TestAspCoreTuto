using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestAspCoreTuto.Bootstrapping.Helpers;
using TestAspCoreTuto.Controllers;

namespace TestAspCoreTuto.Bootstrapping.Extensions
{
    /*
        inject IOptions<T>, IOptionsSnapshot<T> or IOptionsMonitor<T>

        Use IOptions<T> when you are not expecting your config values to change. 

        Use IOptionsSnaphot<T> when you are expecting your values to change but want it to be consistent for the entirety of a request. 
        provides an automatic configuration reloading feature

        Use IOptionsMonitor<T> when you need real time values.

        IOptions, IOptionsMonitor registered with the dependency injection container as a singleton lifetime.
        IOptionsMonitor<TOptions> for binding options classes, consuming inside a singleton service and how it provides automatic configuration reloading feature
    */
    public static class AppSettingsConfigurationExtension
    {
        public static void AddMapConfigurationSectionClasses(this IServiceCollection services, IConfiguration configuration)
        {
            IConfigurationSection appSettingsSection = configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            services.Configure<DashboardHeaderConfiguration>(configuration.GetSection("DashboardSettings:Header"));
            services.AddSingleton<IConfigurationReader, ConfigurationReader>();
        }
    }
}
