using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace TestAspCoreTuto.Bootstrapping.Middlewares
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        const string MessageTemplate = "HTTP {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.0000} ms";

        public RequestResponseLoggingMiddleware(RequestDelegate next, ILogger<object> logger)
        {
            _logger = logger;
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext == null) throw new ArgumentNullException(nameof(httpContext));


            Stopwatch sw = Stopwatch.StartNew();
            try
            {
                await _next(httpContext);

                LogInformation(httpContext, sw);
            }
            catch (Exception ex)
            {
                LogException(httpContext, sw, ex);
            }
        }

        private void LogInformation(HttpContext httpContext, Stopwatch sw)
        {
            sw.Stop();

            int? statusCode = httpContext.Response?.StatusCode;
            _logger.LogDebug(MessageTemplate, httpContext.Request.Method, httpContext.Request.Path, statusCode, sw.Elapsed.TotalMilliseconds);
        }

        private void LogException(HttpContext httpContext, Stopwatch sw, Exception ex)
        {
            sw.Stop();
            _logger.LogCritical(MessageTemplate, httpContext.Request.Method, httpContext.Request.Path, 500, sw.Elapsed.TotalMilliseconds);
        }
    }
}
