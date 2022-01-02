using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using SpendingSummary.Common.QueueBus.Interfaces;
using SpendingSummary.QueueBus.Configuration;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SpendingSummary.QueueBus
{
    public class QueueInitializer : IHostedService
    {
        private readonly IQueueConnection _queueConnection;
        private readonly QueueEventsDefinition _options;

        public QueueInitializer(IQueueConnection queueConnection, IOptions<QueueEventsDefinition> options)
        {
            _queueConnection = queueConnection;
            _options = options.Value;
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
            foreach (var ev in _options.Events.Values)
            {
                await Task.Run(() => {
                    consumerChannel.ExchangeDeclare(ev.Exchange, ExchangeType.Fanout, true);
                    consumerChannel.QueueDeclare(ev.Queue, true, false, false, null);
                    consumerChannel.QueueBind(ev.Queue, ev.Exchange, string.Empty);
                });
            }
        }
    }
}
