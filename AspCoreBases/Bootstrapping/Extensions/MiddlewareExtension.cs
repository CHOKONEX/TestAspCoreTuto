using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using TestAspCoreTuto.Bootstrapping.Middlewares;

namespace TestAspCoreTuto.Bootstrapping.Extensions
{
    public static class MiddlewareExtension
    {
        public static void AddMiddleware(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseMiddleware(typeof(RequestResponseLoggingMiddleware));
            }

            app.UseMiddleware(typeof(ErrorHandlingMiddleware));
        }
    }
}
