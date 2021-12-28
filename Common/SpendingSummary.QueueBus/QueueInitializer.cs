using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using SpendingSummary.Common.QueueBus;
using SpendingSummary.Common.QueueBus.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SpendingSummary.QueueBus
{
    public class QueueInitializer : IHostedService
    {
        private readonly IQueueConnection _queueConnection;

        public QueueInitializer(IQueueConnection queueConnection)
        {
            _queueConnection = queueConnection;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await BindQueuesAsync();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _queueConnection.Dispose();
            return Task.CompletedTask;
        }

        private async Task BindQueuesAsync()
        {
            using var queueChannel = await QueueChannel.CreateAsync(_queueConnection);
            var consumerChannel = queueChannel.GetChannel;
            foreach (var (queue, exchange) in EventDefinitions.ByEventType.Select(x => x.Value))
            {
                await Task.Run(() => {
                    consumerChannel.ExchangeDeclare(exchange, ExchangeType.Fanout, true);
                    consumerChannel.QueueDeclare(queue, true, false, false, null);
                    consumerChannel.QueueBind(queue, exchange, string.Empty);
                });
            }
        }
    }
}
