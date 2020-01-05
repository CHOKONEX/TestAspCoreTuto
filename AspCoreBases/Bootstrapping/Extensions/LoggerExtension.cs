using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace TestAspCoreTuto.Bootstrapping.Extensions
{
    public static class LoggerExtension
    {
        public static void AddLogger(this ILoggingBuilder loggingBuilder, IHostEnvironment env, IConfiguration configuration)
        {
            CreateLogger(configuration);

            loggingBuilder.ClearProviders();
            if (env.IsDevelopment())
            {
                loggingBuilder.AddDebug();
                loggingBuilder.AddConsole();
            }
            loggingBuilder.AddSerilog();
        }

        private static void CreateLogger(IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
               .ReadFrom.Configuration(configuration)
               //.Enrich.FromLogContext()
               //.WriteTo.Console(new RenderedCompactJsonFormatter())
               //.WriteTo.ColoredConsole(
               //       LogEventLevel.Verbose,
               //       "{NewLine}{Timestamp:HH:mm:ss} [{Level}] ({CorrelationToken}) {Message}{NewLine}{Exception}")
               .CreateLogger();
        }
    }
}
