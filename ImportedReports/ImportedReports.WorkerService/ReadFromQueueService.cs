using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SpendingsSummary.Application;
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

        public ReadFromQueueService(ILogger<ReadFromQueueService> logger, IImportReportFromFile importCase)
        {
            _logger = logger;
            _importCase = importCase;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            //await InitializeQueuesAsync();
            _importCase.ImportFileReportToDb(cancellationToken);

            await Task.Delay(5000, cancellationToken);
        }

        private async Task InitializeQueuesAsync() 
        {
            //await _publisher.BindQueueAsync<DataParsedEvent>();
            _logger.LogInformation("Queue for DataParsedEvent has been initialized: {time}", DateTimeOffset.Now);
            return;
        }
    }
}
