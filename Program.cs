using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.IO;
using TestAspCoreTuto.Bootstrapping.Extensions;
using TestAspCoreTuto.Extensions;

namespace TestAspCoreTuto
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IHost host = CreateHostBuilder(args).Build();

            try
            {
                host.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application start-up failed");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                    .ConfigureAppConfiguration((hostingContext, configurationBuilder) =>
                    {
                        configurationBuilder.AddConfiguration(args);
                    })
                    .ConfigureLogging((hostingContext, loggingBuilder) =>
                    {
                        loggingBuilder.AddLogger(hostingContext.HostingEnvironment, hostingContext.Configuration);
                    })
                    .ConfigureWebHostDefaults(webBuilder =>
                    {
                        webBuilder.UseStartup<Startup>();
                    });
        }
    }
}
