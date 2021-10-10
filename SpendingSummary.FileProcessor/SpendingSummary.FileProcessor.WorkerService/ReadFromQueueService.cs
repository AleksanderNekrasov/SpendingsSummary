using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SpendingsSummary.Application;
using SpendingSummary.Common.Interfaces;
using SpendingSummary.Common.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SpendingsSummary.WorkerService
{
    public class ReadFromQueueService : BackgroundService
    {
        private readonly ILogger<ReadFromQueueService> _logger;
        private readonly IImportReportFromFile _importCase;
        private readonly IQueuePublisher _publisher;

        public ReadFromQueueService(ILogger<ReadFromQueueService> logger, IImportReportFromFile importCase, IQueuePublisher publisher)
        {
            _logger = logger;
            _importCase = importCase;
            _publisher = publisher;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                _publisher.Publish(new DataParsedEvent { TransactionId = Guid.NewGuid() });
                _importCase.ImportFileReportToDb();

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
