using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using TestAspCoreTuto.Bootstrapping.Authorizations;
using TestAspCoreTuto.Bootstrapping.Extensions;
using TestAspCoreTuto.Extensions;

namespace TestAspCoreTuto.Bootstrapping
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder =>
                    {
                        builder
                            .AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });
            services.AddRouting(options => options.LowercaseUrls = true);
            services.AddControllers(options =>
            {
                //TODO +WORK
                //options.Conventions.Add(new AddAuthorizeFiltersControllerConvention());
                //TODO +WORK
            });
            services.AddActionFilter();
            services.AddSwagger();
            services.AddCompression();
            services.AddInjections();
            services.AddJobsInjections();
            services.AddHostedServices();
            services.AddMigratorExtension(Configuration);
            services.AddMapConfigurationSectionClasses(Configuration);
            services.AddAuthentification(Configuration);
            services.AddAuthorization(Configuration);

            services.AddMvc(config =>
            {
                // Add XML Content Negotiation
                config.RespectBrowserAcceptHeader = true;
            });
            //TODO +WORK
            //services.AddMvc(config =>
            //{
            //    var policy = new AuthorizationPolicyBuilder()
            //        .RequireAuthenticatedUser()
            //        .Build();
            //    config.Filters.Add(new AuthorizeFilter(policy));
            //});
            //TODO +WORK

            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ApiVersionReader = new HeaderApiVersionReader("api-version");
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<object> logger, IHostApplicationLifetime applicationLifetime, IServiceProvider services)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
                //app.ConfigureExceptionHandler(logger);
            }

            app.AddMiddleware(env);

            //app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors("AllowAllOrigins");
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseCustomSwagger();
            app.UseResponseCompression();

            applicationLifetime.ApplicationStarted.Register(() => OnStarted(logger));
            applicationLifetime.ApplicationStopping.Register(() => OnShutdown(logger));
        }

        private static void OnShutdown(ILogger logger)
        {
            logger.LogWarning("Application Ended");
        }

        private static void OnStarted(ILogger logger)
        {
            string environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            logger.LogWarning($"Starting up {environmentName}");
        }
    }
}
