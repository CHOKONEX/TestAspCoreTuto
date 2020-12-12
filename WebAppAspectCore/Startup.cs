using System;
using AspectCore.Configuration;
using AspectCore.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebAppAspectCore.Models;
using WebAppAspectCore.Services;

namespace WebAppAspectCore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.ConfigureDynamicProxy(config =>
            //{
            //    config.Interceptors.AddDelegate((ctx, next) => next(ctx));
            //});
            //services.ConfigureDynamicProxy(config => config.Interceptors.AddTyped<InterceptAttribute>());
            //services.ConfigureDynamicProxy(config => config.Interceptors.AddTyped<ScopeIntercept>());

            //return serviceCollection.BuildDynamicProxyProvider();

            services.AddSingleton<IService, Service>();

            services.AddControllers();
            
            //services.BuildAspectInjectorProvider();

            //services.AddDynamicProxy(config => config.Interceptors.AddTyped<InterceptAttribute>());
            //var serviceProvider = services.BuildDynamicProxyProvider();
            ////var serviceProvider = services.BuildServiceProvider();
            //IService service = serviceProvider.GetService<IService>();
            //string ch = service.GetValue("azerty");
            //Console.WriteLine(ch);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            app.UseMiddleware<LoggingMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
