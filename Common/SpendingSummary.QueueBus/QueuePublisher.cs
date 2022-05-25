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
using SpendingSummary.QueueBus.Interfaces;

namespace SpendingSummary.Common.QueueBus
{
    public sealed class QueuePublisher: QueueBusBase, IRequestHandler<PublishEventCommand>
    {
        private readonly IQueueChannels _channels;
        private readonly ILogger<QueuePublisher> _logger;


        public QueuePublisher(IQueueChannels channels, IOptions<QueueEventsDefinition> options, ILogger<QueuePublisher> logger)
            : base(options)
        {
            _channels = channels;
            _logger = logger;
        }

        public async Task<Unit> Handle(PublishEventCommand request, CancellationToken cancellationToken)
        {
            await PublishAsync(request.Event, cancellationToken);
            return Unit.Value;
        }

        private async Task PublishAsync(IQueueEvent queueEvent, CancellationToken cancellationToken)
        {
            using var channel = await _channels.CreateAsync();
            var eventDefinition = GetEventByType(queueEvent);

            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(queueEvent, queueEvent.GetType()));

            var properties = channel.CreateBasicProperties();
            properties.DeliveryMode = 2;

            channel.BasicPublish(eventDefinition.Exchange, eventDefinition.Name, true, properties, body);
        }
    }
}
