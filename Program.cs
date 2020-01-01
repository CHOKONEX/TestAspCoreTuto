using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;
using System;
using System.IO;

namespace TestAspCoreTuto
{
    public class Program
    {
        private static IConfigurationBuilder builtConfig;

        public static void Main(string[] args)
        {
            string executableLocation = AppContext.BaseDirectory;
            string pathOfCommonSettingsFile = Path.Combine(executableLocation, "Properties");

            builtConfig = new ConfigurationBuilder()
            .AddJsonFile(Path.Combine(pathOfCommonSettingsFile, "logSettings.json"), optional: true)
            .AddCommandLine(args)
            ;

            Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builtConfig.Build())
            //.Enrich.FromLogContext()
            //.WriteTo.Console(new RenderedCompactJsonFormatter())
            //.WriteTo.ColoredConsole(
            //       LogEventLevel.Verbose,
            //       "{NewLine}{Timestamp:HH:mm:ss} [{Level}] ({CorrelationToken}) {Message}{NewLine}{Exception}")
            .CreateLogger();

            try
            {
                string environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                Log.Information($"Starting up {environmentName}");

                CreateHostBuilder(args).Build().Run();
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
                    .ConfigureAppConfiguration((hostingContext, config) =>
                    {
                        string environmentName = hostingContext.HostingEnvironment.EnvironmentName;
                        Log.Information($"Set config {environmentName}");
                        config.AddConfiguration(builtConfig.Build());
                    })
                    .ConfigureLogging((hostingContext, logging) =>
                    {
                        logging.ClearProviders();
                        logging.AddDebug();
                        logging.AddConsole();
                        logging.AddSerilog();
                    })
                    .ConfigureWebHostDefaults(webBuilder =>
                    {
                        webBuilder.UseStartup<Startup>();
                    });
        }
    }
}
