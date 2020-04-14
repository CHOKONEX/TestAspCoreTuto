using App.Core.Infra.SqlResourcesReader;
using Asp.Core.Common.AssemblyFileReader;
using Microsoft.Extensions.DependencyInjection;
using TestAspCoreTuto.Tests.Test1;

namespace TestAspCoreTuto.Bootstrapping.Extensions
{
    public static class InjectionExtension
    {
        public static void AddInjections(this IServiceCollection services)
        {
            AddInjectionsFromInfra(services);

            services.AddSingleton<IUserService, UserService>();
        }

        private static void AddInjectionsFromInfra(IServiceCollection services)
        {
            services.AddSingleton<IAssemblyResourceReader, AssemblyResourceReader>();
            services.AddSingleton<ISqlFileQueryReader, SqlFileQueryReader>();
        }
    }
}
