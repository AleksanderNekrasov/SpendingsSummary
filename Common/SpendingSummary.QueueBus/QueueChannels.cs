using RabbitMQ.Client;
using SpendingSummary.Common.QueueBus.Interfaces;
using SpendingSummary.QueueBus.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace SpendingSummary.QueueBus
{
    internal sealed class QueueChannels : IQueueChannels
    {
        private readonly object _lock = new object();
        private readonly ConcurrentDictionary<int, IModel> _channels = new();
        private readonly IQueueConnection _connection;

        public QueueChannels(IQueueConnection connection)
        {
            _connection = connection;
        }

        public async Task<IModel> CreateAsync()
        {
            var threadId = Thread.CurrentThread.ManagedThreadId;
            if (_channels.TryGetValue(threadId, out var channel))
            {
                return channel;
            }

            while (!_connection.IsOpen)
            {
                await _connection.TryConnectAsync();
            }

            var createdChannel = _connection.CreateModel();
            _channels.TryAdd(threadId, createdChannel);

            return createdChannel;
        }

        public void Dispose()
        {
            _connection.Dispose();
        }
    }
}
