using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.IO;
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
                string environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                Log.Warning($"Starting up {environmentName}");

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
                        InitConfiguration(args, hostingContext, configurationBuilder);
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

        private static void InitConfiguration(string[] args, HostBuilderContext hostBuilderContext, IConfigurationBuilder configurationBuilder)
        {
            string executableLocation = AppContext.BaseDirectory;
            string pathOfCommonSettingsFile = Path.Combine(executableLocation, "Properties");

            IConfigurationBuilder builtConfig = new ConfigurationBuilder()
            .AddJsonFile(Path.Combine(pathOfCommonSettingsFile, "logSettings.json"), optional: true)
            .AddCommandLine(args)
            ;

            configurationBuilder.AddConfiguration(builtConfig.Build());
        }
    }
}
