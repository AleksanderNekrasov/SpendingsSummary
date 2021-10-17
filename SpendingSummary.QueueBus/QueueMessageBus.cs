using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using SpendingSummary.Common.Interfaces;
using SpendingSummary.Queue.Interfaces;

namespace SpendingSummary.Queue
{
    public abstract class QueueMessageBus : IQueueMessageBus
    {
        protected readonly IQueueConnection _queueConnection;
        protected readonly IServiceScopeFactory _serviceScopeFactory;
        protected IModel _consumerChannel;

        public QueueMessageBus(IQueueConnection persistentConnection, IServiceScopeFactory serviceScopeFactory)
        {
            _queueConnection = persistentConnection;
            _serviceScopeFactory = serviceScopeFactory;
            _consumerChannel = _queueConnection.CreateModel();
        }

        public void BindQueue<T>() where T : IQueueEvent
        {
            var eventDefinition = EventDefinitions.ByEventType[typeof(T)];
            _consumerChannel.ExchangeDeclare(eventDefinition.exchange, ExchangeType.Fanout, true);
            _consumerChannel.QueueDeclare(eventDefinition.queue, true, false, false, null);
            _consumerChannel.QueueBind(eventDefinition.queue, eventDefinition.exchange, string.Empty);
        }

    }
}
