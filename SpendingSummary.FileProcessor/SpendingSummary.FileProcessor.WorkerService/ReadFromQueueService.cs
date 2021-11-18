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
            //await InitializeQueuesAsync();
            _importCase.ImportFileReportToDb();

            await Task.Delay(5000, stoppingToken);
        }

        private async Task InitializeQueuesAsync() 
        {
            await _publisher.BindQueueAsync<DataParsedEvent>();
            _logger.LogInformation("Queue for DataParsedEvent has been initialized: {time}", DateTimeOffset.Now);
            return;
        }
    }
}
