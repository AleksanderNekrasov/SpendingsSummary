using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using SpendingSummary.Common.QueueBus.Interfaces;
using SpendingSummary.QueueBus.Configuration;
using SpendingSummary.QueueBus.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SpendingSummary.QueueBus
{
    public class QueueInitializer : IHostedService
    {
        private readonly QueueEventsDefinition _options;
        private readonly IQueueChannels _channels;

        public QueueInitializer(IOptions<QueueEventsDefinition> options, IQueueChannels channels)
        {
            _options = options.Value;
            _channels = channels;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await BindQueuesAsync();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _channels.Dispose();
            return Task.CompletedTask;
        }

        private async Task BindQueuesAsync()
        {
            using var queueChannel = await _channels.CreateAsync();

            var declare = true; // _options.Queue?.Declare ?? true;
            var durable = true; // _options.Queue?.Durable ?? true;
            var exclusive = false; // _options.Queue?.Exclusive ?? false;
            var autoDelete = false; // _options.Queue?.AutoDelete ?? false;

            foreach (var ev in _options.Events.Values)
            {
                //this method is not called from API endpoint and will be called on application start-up only,
                //thus there will be no scalability issues from using threads
                await Task.Run(() => {
                    queueChannel.ExchangeDeclare(ev.Exchange, ExchangeType.Fanout, true);
                    queueChannel.QueueDeclare(ev.Queue, durable, exclusive, autoDelete, new Dictionary<string, object>());
                    queueChannel.QueueBind(ev.Queue, ev.Exchange, string.Empty);
                });
            }
        }
    }
}
