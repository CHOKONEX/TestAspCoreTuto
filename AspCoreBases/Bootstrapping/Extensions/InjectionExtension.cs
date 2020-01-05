using Microsoft.Extensions.DependencyInjection;
using TestAspCoreTuto.Tests.Test1;

namespace TestAspCoreTuto.Bootstrapping.Extensions
{
    public static class InjectionExtension
    {
        public static void AddInjections(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
        }
    }
}
