using Microsoft.Extensions.Logging;
using Quartz;
using System.Threading.Tasks;

namespace TestAspCoreTuto.Jobs
{
    [DisallowConcurrentExecution]
    public class EmailJob : IJob
    {
        private readonly ILogger<HelloWorldJob> _logger;
        public EmailJob(ILogger<HelloWorldJob> logger)
        {
            _logger = logger;
        }

        public Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("Send Email!");
            return Task.CompletedTask;
        }
    }
}
