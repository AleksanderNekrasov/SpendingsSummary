using Microsoft.Extensions.DependencyInjection;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SpendingSummary.Common.Interfaces;
using SpendingSummary.Queue.Interfaces;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SpendingSummary.Queue
{
    public class QueueSubscriber : QueueMessageBus, IQueueSubscriber
    {
        public QueueSubscriber(IQueueConnection persistentConnection, IServiceScopeFactory serviceScopeFactory)
            : base(persistentConnection, serviceScopeFactory)
        {
        }

        public void Subscribe<T>(string queueName) where T : IQueueEvent
        {
            StartSubscribing<T>(queueName);
        }

        private void StartSubscribing<T>(string queueName) where T : IQueueEvent
        {
            var consumer = new AsyncEventingBasicConsumer(_consumerChannel);
            _consumerChannel.BasicConsume(queueName, false, consumer);
            consumer.Received += MessageReceived<T>;
        }

        private async Task MessageReceived<T>(object sender, BasicDeliverEventArgs eventArgs) where T : IQueueEvent
        {
            var message = Encoding.UTF8.GetString(eventArgs.Body.Span.ToArray());
            await ProcessEvent<T>(message);
        }

        private async Task ProcessEvent<T>(string message) where T : IQueueEvent
        {
            using var scope = _serviceScopeFactory.CreateScope();

            var handler = scope.ServiceProvider.GetRequiredService<IQueueEventHandler<T>>();

            if (handler == null)
            {
                return;
            }

            var queueEvent = DeserializeObject<T>(message);
            await handler.HandleAsync(queueEvent);
        }

        private T DeserializeObject<T>(string json) => JsonSerializer.Deserialize<T>(json);
    }
}
