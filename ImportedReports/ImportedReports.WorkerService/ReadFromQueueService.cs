using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SpendingSummary.Common.Interfaces;
using SpendingSummary.Common.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SpendingsSummary.WorkerService
{
    public sealed class ReadFromQueueService : BackgroundService
    {
        private readonly ILogger<ReadFromQueueService> _logger;
        private readonly IQueueSubscriber _queueSubscriber;

        public ReadFromQueueService(ILogger<ReadFromQueueService> logger, IQueueSubscriber queueSubscriber)
        {
            _logger = logger;
            _queueSubscriber = queueSubscriber;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken) =>
            await _queueSubscriber.StartSubscribingAsync<DataUploadedEvent>();        
    }
}
