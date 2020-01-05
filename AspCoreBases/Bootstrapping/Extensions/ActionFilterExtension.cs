﻿using Microsoft.Extensions.DependencyInjection;
using TestAspCoreTuto.Bootstrapping.Attributes;

namespace TestAspCoreTuto.Bootstrapping.Extensions
{
    public static class ActionFilterExtension
    {
        public static void AddActionFilter(this IServiceCollection services)
        {
            services.AddScoped<ModelValidationAttribute>();
        }
    }
}
