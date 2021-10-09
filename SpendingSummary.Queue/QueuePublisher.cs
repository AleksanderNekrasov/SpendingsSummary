using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using SpendingSummary.Common.Interfaces;
using SpendingSummary.Queue.Interfaces;
using System.Text;

namespace SpendingSummary.Queue
{
    public class QueuePublisher : QueueMessageBus
    {
        public QueuePublisher(IQueueConnection persistentConnection, IServiceScopeFactory serviceScopeFactory) 
            : base(persistentConnection, serviceScopeFactory)
        {
        }

        public void Publish(IQueueEvent queueEvent)
        {
            if (!_queueConnection.IsOpen)
            {
                _queueConnection.TryConnect();
            }

            var eventyType = queueEvent.GetType();

            using var channel = _queueConnection.CreateModel();
            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(queueEvent));

            var properties = channel.CreateBasicProperties();
            properties.DeliveryMode = 2;

            channel.BasicPublish(BrockerNames.ByEventType[eventyType].exchange, eventyType.Name, true, properties, body);
        }
    }
}
