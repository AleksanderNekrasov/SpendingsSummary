using RabbitMQ.Client;
using SpendingSummary.Common.QueueBus.Interfaces;
using System;
using System.Threading.Tasks;

namespace SpendingSummary.QueueBus
{
    internal class QueueChannel : IDisposable
    {
        private IModel _channel;

        private QueueChannel(IModel channel) 
        {
            _channel = channel;
        }

        internal static async Task<QueueChannel> CreateAsync(IQueueConnection queueConnection) 
        {
            while (!queueConnection.IsOpen)
            {
                await queueConnection.TryConnectAsync();
            }

            return new QueueChannel(queueConnection.CreateModel());
        }

        internal IModel GetChannel => _channel;

        public void Dispose()
        {
            _channel.Dispose();
        }
    }
}
