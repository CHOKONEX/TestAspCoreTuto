using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TestAspCoreTuto.Extensions;
using Microsoft.Extensions.Logging;
using Serilog;
using TestAspCoreTuto.Bootstrapping.Middlewares;

namespace TestAspCoreTuto
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

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
            services.AddControllers();
            services.AddSwagger();
            services.AddCompression();
            //services.AddApiVersioning(
            //    config =>
            //    {
            //        config.ReportApiVersions = true;
            //        config.AssumeDefaultVersionWhenUnspecified = true;
            //        config.DefaultApiVersion = new ApiVersion(1, 0);
            //        config.ApiVersionReader = new HeaderApiVersionReader("api-version");
            //    });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<object> logger)
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

            app.UseMiddleware(typeof(ErrorHandlingMiddleware));

            //app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors("AllowAllOrigins");
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseCustomSwagger();
            app.UseResponseCompression();
        }
    }
}
