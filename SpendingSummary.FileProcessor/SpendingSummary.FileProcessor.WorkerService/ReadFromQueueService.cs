using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SpendingsSummary.Application;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SpendingsSummary.WorkerService
{
    public class ReadFromQueueService : BackgroundService
    {
        private readonly ILogger<ReadFromQueueService> _logger;
        private readonly IImportReportFromFile _importCase;

        public ReadFromQueueService(ILogger<ReadFromQueueService> logger, IImportReportFromFile importCase)
        {
            _logger = logger;
            _importCase = importCase;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                _importCase.ImportFileReportToDb();

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
