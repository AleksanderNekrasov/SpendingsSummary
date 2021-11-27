using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using SpendingSummary.Common.Interfaces;
using System.Text;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using SpendingSummary.Common.QueueBus.Interfaces;

namespace SpendingSummary.Common.QueueBus
{
    public class QueuePublisher : QueueMessageBus, IQueuePublisher
    {
        private readonly ILogger<QueuePublisher> _logger;

        public QueuePublisher(IQueueConnection persistentConnection, IServiceScopeFactory serviceScopeFactory, ILogger<QueuePublisher> logger)
            : base(persistentConnection, serviceScopeFactory, logger)
        {
            _logger = logger;
        }

        public async Task PublishAsync(IQueueEvent queueEvent)
        {
            var channel = await GetOrCreateModelAsync();
            var eventyType = queueEvent.GetType();

            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(queueEvent));

            var properties = channel.CreateBasicProperties();
            properties.DeliveryMode = 2;

            channel.BasicPublish(EventDefinitions.ByEventType[eventyType].exchange, eventyType.Name, true, properties, body);
        }
    }
}
