using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using SpendingSummary.Queue.Interfaces;

namespace SpendingSummary.Queue
{
    public abstract class QueueMessageBus
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
    }
}
