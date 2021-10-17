using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SpendingSummary.Common.Interfaces;
using SpendingSummary.Common.Models;
using System.Threading;
using System.Threading.Tasks;

namespace SpendingSummary.DataStore.WorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IQueueSubscriber _subscriber;

        public Worker(ILogger<Worker> logger, IQueueSubscriber subscriber)
        {
            _logger = logger;
            _subscriber = subscriber;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _subscriber.BindQueue<DataParsedEvent>();
            _subscriber.Subscribe<DataParsedEvent>();
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _subscriber.Dispose();
        }
    }
}
