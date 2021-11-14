using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using SpendingSummary.Common.Interfaces;
using SpendingSummary.Queue.Interfaces;
using System.Threading.Tasks;

namespace SpendingSummary.Queue
{
    public abstract class QueueMessageBus : IQueueMessageBus
    {
        protected readonly IQueueConnection _queueConnection;
        protected readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger _logger;
        protected IModel _consumerChannel;

        public QueueMessageBus(IQueueConnection persistentConnection, IServiceScopeFactory serviceScopeFactory, ILogger logger)
        {
            _queueConnection = persistentConnection;
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
        }

        public async Task BindQueueAsync<T>() where T : IQueueEvent
        {
            await GetOrCreateModelAsync();
            var eventDefinition = EventDefinitions.ByEventType[typeof(T)];
            _consumerChannel.ExchangeDeclare(eventDefinition.exchange, ExchangeType.Fanout, true);
            _consumerChannel.QueueDeclare(eventDefinition.queue, true, false, false, null);
            _consumerChannel.QueueBind(eventDefinition.queue, eventDefinition.exchange, string.Empty);
        }

        protected async Task<IModel> GetOrCreateModelAsync() 
        {
            if (_consumerChannel != null) 
            {
                return _consumerChannel;
            }

            while (!_queueConnection.IsOpen)
            {
                _logger.LogInformation("Queue is not yet ready");
                await _queueConnection.TryConnectAsync();
            }

            _consumerChannel = _queueConnection.CreateModel();
            return _consumerChannel;
        }
    }
}
