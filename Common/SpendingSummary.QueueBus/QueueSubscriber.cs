using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SpendingSummary.Common.Interfaces;
using SpendingSummary.QueueBus;
using SpendingSummary.QueueBus.Configuration;
using SpendingSummary.QueueBus.Interfaces;
using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SpendingSummary.Common.QueueBus
{
    public class QueueSubscriber : QueueBusBase, IQueueSubscriber, IDisposable
    {
        private readonly IQueueChannels _channels;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private bool _disposed;

        public QueueSubscriber(IQueueChannels channels, IServiceScopeFactory serviceScopeFactory, IOptions<QueueEventsDefinition> options, ILogger<QueueSubscriber> logger)
            : base(options)
        {
            _channels = channels;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task StartSubscribingAsync<T>() where T : IQueueEvent
        {
            if (_disposed) throw new ObjectDisposedException("QueueSubscriber");

            var channel = await _channels.CreateAsync();

            //AsyncEventingBasicConsumer does not work for unknown reason
            var consumer = new EventingBasicConsumer(channel);
            var ev = GetEventByType(typeof(T));
            channel.BasicConsume(ev.Queue, true, consumer);
            consumer.Received += async (o, eventArgs) =>
            {
                var message = Encoding.UTF8.GetString(eventArgs.Body.Span.ToArray());
                await ProcessEvent<T>(message);
            };
        }

        private async Task ProcessEvent<T>(string message) where T : IQueueEvent
        {
            using var scope = _serviceScopeFactory.CreateScope();

            var handler = scope.ServiceProvider.GetRequiredService<IQueueEventHandler<T>>();

            if (handler == null)
            {
                return;
            }

            await handler.HandleQueueEventAsync(DeserializeObject<T>(message));
        }

        private T DeserializeObject<T>(string json) => JsonSerializer.Deserialize<T>(json);

        public void Dispose()
        {
            try
            {
                _channels.Dispose();
            }
            catch
            {
                // ignored
            }

            _disposed = true;
        }
    }
}
