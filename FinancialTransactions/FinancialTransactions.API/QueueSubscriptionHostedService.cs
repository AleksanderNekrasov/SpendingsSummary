using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SpendingSummary.Common.Interfaces;
using SpendingSummary.Common.Models;
using System.Threading;
using System.Threading.Tasks;

namespace SpendingSummary.DataStore.WorkerService
{
    public class QueueSubscriptionHostedService : BackgroundService
    {
        private readonly ILogger<QueueSubscriptionHostedService> _logger;
        private readonly IQueueSubscriber _subscriber;

        public QueueSubscriptionHostedService(ILogger<QueueSubscriptionHostedService> logger, IQueueSubscriber subscriber)
        {
            _logger = logger;
            _subscriber = subscriber;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _subscriber.StartSubscribingAsync<DataParsedEvent>();
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
        }
    }
}
