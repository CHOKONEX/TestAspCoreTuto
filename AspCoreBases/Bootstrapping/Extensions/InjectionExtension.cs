using App.Core.Infra;
using Asp.Core.Common.AssemblyFileReader;
using Microsoft.Extensions.DependencyInjection;
using TestAspCoreTuto.Tests.Test1;

namespace TestAspCoreTuto.Bootstrapping.Extensions
{
    public static class InjectionExtension
    {
        public static void AddInjections(this IServiceCollection services)
        {
            AddInjectionsFromCore(services);
            InfraInjectionModule.AddInjections(services);

            services.AddSingleton<IUserService, UserService>();
        }

        private static void AddInjectionsFromCore(IServiceCollection services)
        {
            services.AddSingleton<IAssemblyResourceReader, AssemblyResourceReader>();
        }
    }
}