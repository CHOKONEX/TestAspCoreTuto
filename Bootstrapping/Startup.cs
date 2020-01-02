using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Reflection;
using System.IO;
using TestAspCoreTuto.Extensions;
using Microsoft.Extensions.Logging;
using Serilog;
using Microsoft.AspNetCore.ResponseCompression;
using System.IO.Compression;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace TestAspCoreTuto
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
            services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));
            //services.AddApiVersioning(
            //    config =>
            //    {
            //        config.ReportApiVersions = true;
            //        config.AssumeDefaultVersionWhenUnspecified = true;
            //        config.DefaultApiVersion = new ApiVersion(1, 0);
            //        config.ApiVersionReader = new HeaderApiVersionReader("api-version");
            //    });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
                //app.UseExceptionHandler(errorApp =>
                //{
                //    errorApp.Run(async context =>
                //    {
                //        context.Response.StatusCode = 500;
                //        context.Response.ContentType = "text/plain";
                //        var errorFeature = context.Features.Get<IExceptionHandlerFeature>();
                //        if (errorFeature != null)
                //        {
                //            var logger = loggerFactory.CreateLogger("Global exception logger");
                //            logger.LogError(500, errorFeature.Error, errorFeature.Error.Message);
                //        }

                //        await context.Response.WriteAsync("There was an error");
                //    });
                //});
            }

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
