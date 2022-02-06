using System.Text.Json;
using SpendingSummary.Common.Interfaces;
using System.Text;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using SpendingSummary.Common.QueueBus.Interfaces;
using MediatR;
using SpendingSummary.QueueBus;
using System.Threading;
using Microsoft.Extensions.Options;
using SpendingSummary.QueueBus.Configuration;

namespace SpendingSummary.Common.QueueBus
{
    public class QueuePublisher: QueueBusBase, IRequestHandler<PublishEventCommand>
    {
        private readonly ILogger<QueuePublisher> _logger;
        private IQueueConnection _queueConnection;

        public QueuePublisher(IQueueConnection queueConnection, IOptions<QueueEventsDefinition> options, ILogger<QueuePublisher> logger)
            : base(options)
        {
            _queueConnection = queueConnection;
            _logger = logger;
        }

        public async Task<Unit> Handle(PublishEventCommand request, CancellationToken cancellationToken)
        {
            await PublishAsync(request.Event, cancellationToken);
            return Unit.Value;
        }

        private async Task PublishAsync(IQueueEvent queueEvent, CancellationToken cancellationToken)
        {
            using var queueChannel = await QueueChannel.CreateAsync(_queueConnection);
            var channel = queueChannel.GetChannel;
            var eventDefinition = GetEventByType(queueEvent);

            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(queueEvent, queueEvent.GetType()));

            var properties = channel.CreateBasicProperties();
            properties.DeliveryMode = 2;

            channel.BasicPublish(eventDefinition.Exchange, eventDefinition.Name, true, properties, body);
        }
    }
}
