using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SpendingSummary.Common.Interfaces;
using SpendingSummary.Queue.Interfaces;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace SpendingSummary.Queue
{
    public class QueueSubscriber : QueueMessageBus, IQueueSubscriber
    {
        private bool _disposed;
        private IList<string> _consumeTags;

        public QueueSubscriber(IQueueConnection persistentConnection, IServiceScopeFactory serviceScopeFactory, ILogger<QueueSubscriber> logger)
            : base(persistentConnection, serviceScopeFactory, logger)
        {
            _consumeTags = new List<string>();
        }

        public void Subscribe<T>() where T : IQueueEvent
        {
            StartSubscribing<T>(EventDefinitions.ByEventType[typeof(T)].queue);
        }

        public void UnsubscribeAll()
        {
            foreach (var tag in _consumeTags)
            {
                _consumerChannel.BasicCancelNoWait(tag);
            }
        }

        private void StartSubscribing<T>(string queueName) where T : IQueueEvent
        {
            var consumer = new EventingBasicConsumer(_consumerChannel);
            _consumeTags.Add(_consumerChannel.BasicConsume(queueName, false, consumer));
            consumer.Received += MessageReceived<T>;
        }

        private void MessageReceived<T>(object sender, BasicDeliverEventArgs eventArgs) where T : IQueueEvent
        {
            var message = Encoding.UTF8.GetString(eventArgs.Body.Span.ToArray());
            ProcessEvent<T>(message);
        }

        private void ProcessEvent<T>(string message) where T : IQueueEvent
        {
            using var scope = _serviceScopeFactory.CreateScope();

            var handler = scope.ServiceProvider.GetRequiredService<IQueueEventHandler<T>>();

            if (handler == null)
            {
                return;
            }

            var queueEvent = DeserializeObject<T>(message);
            handler.HandleAsync(queueEvent);
        }

        private T DeserializeObject<T>(string json) => JsonSerializer.Deserialize<T>(json);

        public void Dispose()
        {
            UnsubscribeAll();
            _consumerChannel.Dispose();
            _consumerChannel = null;
            _disposed = true;
        }
    }
}
