using Asp.Core.Attributes;
using Microsoft.Extensions.DependencyInjection;
using NetCore.AutoRegisterDi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace App.Core.Infra
{
    public static class InfraInjectionModule
    {
        public static void AddInjections(IServiceCollection services)
        {
            IEnumerable<Type> singletons = Assembly.GetExecutingAssembly().GetTypes()
                .Where(x => x.IsPublic && x.GetCustomAttributes<SingletonAttribute>() != null);
            
            services.RegisterAssemblyPublicNonGenericClasses(Assembly.GetExecutingAssembly())
                .Where(type => singletons.Contains(type))
                .AsPublicImplementedInterfaces(ServiceLifetime.Singleton);
        }
    }
}
