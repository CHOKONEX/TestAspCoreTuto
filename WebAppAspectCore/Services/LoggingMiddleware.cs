using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace WebAppAspectCore.Services
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<LoggingMiddleware> _logger;
        private readonly int instanceHashCode;

        public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
        {
            this.next = next;
            this.instanceHashCode = this.GetHashCode();
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            // Code exécuté avant le middleware suivant
            _logger.LogDebug("Executing logging middleware (HashCode:{0})...",
            this.instanceHashCode);

            var stopWatch = new Stopwatch();
            stopWatch.Start();

            await this.next(context); // Appel au middleware suivant 

            // Code exécuté après le middleware suivant
            stopWatch.Stop();
            var executionTime = stopWatch.Elapsed;

            _logger.LogDebug("Logging middleware executed ({0} ms) (HashCode:{1}.",
            executionTime.Milliseconds, this.instanceHashCode);

            var client = new MemoryMetricsClient();
            var metrics = client.GetMetrics();

            Console.WriteLine("Total: " + metrics.Total);
            Console.WriteLine("Used : " + metrics.Used);
            Console.WriteLine("Free : " + metrics.Free);

        }
    }
}
