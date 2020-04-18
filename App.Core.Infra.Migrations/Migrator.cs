using FluentMigrator;
using Microsoft.Extensions.DependencyInjection;
using NetCore.AutoRegisterDi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace App.Core.Infra.Migrations
{
    public static class MigratorInjetionModule
    {
        public static void AddInjections(IServiceCollection services)
        {
            IEnumerable<Type> migrators = Assembly.GetExecutingAssembly().GetTypes()
                .Where(x => x.IsPublic && x.IsAssignableFrom(typeof(Migration)));

            services.RegisterAssemblyPublicNonGenericClasses(Assembly.GetExecutingAssembly())
                .Where(type => migrators.Contains(type))
                .AsPublicImplementedInterfaces();
        }
    }
}
