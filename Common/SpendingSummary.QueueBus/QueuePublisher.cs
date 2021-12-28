using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using SpendingSummary.Common.Interfaces;
using System.Text;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using SpendingSummary.Common.QueueBus.Interfaces;
using MediatR;
using SpendingSummary.QueueBus;
using System.Threading;

namespace SpendingSummary.Common.QueueBus
{
    public class QueuePublisher: IRequestHandler<PublishEventCommand>
    {
        private readonly ILogger<QueuePublisher> _logger;
        private IQueueConnection _queueConnection;

        public QueuePublisher(IQueueConnection queueConnection, ILogger<QueuePublisher> logger)
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
            var eventyType = queueEvent.GetType();

            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(queueEvent));

            var properties = channel.CreateBasicProperties();
            properties.DeliveryMode = 2;

            await Task.Run(() => channel.BasicPublish(EventDefinitions.ByEventType[eventyType].exchange, eventyType.Name, true, properties, body), cancellationToken);
        }
    }
}
