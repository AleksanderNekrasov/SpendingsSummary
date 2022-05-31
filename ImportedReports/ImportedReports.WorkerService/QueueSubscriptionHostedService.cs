using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SpendingSummary.Common.Interfaces;
using SpendingSummary.Common.Models;
using System.Threading;
using System.Threading.Tasks;

namespace SpendingsSummary.WorkerService
{
    public sealed class QueueSubscriptionHostedService : BackgroundService
    {
        private readonly ILogger<QueueSubscriptionHostedService> _logger;
        private readonly IQueueSubscriber _queueSubscriber;

        public QueueSubscriptionHostedService(ILogger<QueueSubscriptionHostedService> logger, IQueueSubscriber queueSubscriber)
        {
            _logger = logger;
            _queueSubscriber = queueSubscriber;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken) =>
            await _queueSubscriber.StartSubscribingAsync<DataUploadedEvent>();        
    }
}
