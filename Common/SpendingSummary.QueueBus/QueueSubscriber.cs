﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SpendingSummary.Common.Interfaces;
using SpendingSummary.Common.QueueBus.Interfaces;
using SpendingSummary.QueueBus;
using SpendingSummary.QueueBus.Configuration;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace SpendingSummary.Common.QueueBus
{
    public class QueueSubscriber : QueueBusBase, IQueueSubscriber, IDisposable
    {
        private readonly IQueueConnection _connection;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private bool _disposed;
        private IList<string> _consumeTags;
        private readonly ConcurrentBag<IModel> _channels = new();

        public QueueSubscriber(IQueueConnection connection, IServiceScopeFactory serviceScopeFactory, IOptions<QueueEventsDefinition> options, ILogger<QueueSubscriber> logger)
            : base(options)
        {
            _consumeTags = new List<string>();
            _connection = connection;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task StartSubscribingAsync<T>() where T : IQueueEvent
        {
            if (_disposed) throw new ObjectDisposedException("QueueSubscriber");

            var queueChannel = await QueueChannel.CreateAsync(_connection);
            var channel = queueChannel.GetChannel;
            _channels.Add(channel);

            //AsyncEventingBasicConsumer does not work for unknown reason
            var consumer = new EventingBasicConsumer(channel);
            var ev = GetEventByType(typeof(T));
            _consumeTags.Add(channel.BasicConsume(ev.Queue, true, consumer));
            consumer.Received += async (o, eventArgs) =>
            {
                var message = Encoding.UTF8.GetString(eventArgs.Body.Span.ToArray());
                await ProcessEvent<T>(message);
            };
        }

        private async Task ProcessEvent<T>(string message) where T : IQueueEvent
        {
            using var scope = _serviceScopeFactory.CreateScope();

            var handler = scope.ServiceProvider.GetRequiredService<IQueueEventHandler<T>>();

            if (handler == null)
            {
                return;
            }

            await handler.HandleQueueEventAsunc(DeserializeObject<T>(message));
        }

        private T DeserializeObject<T>(string json) => JsonSerializer.Deserialize<T>(json);

        public void Dispose()
        {
            foreach (var channel in _channels) channel.Dispose();
            _channels.Clear();

            try
            {
                _connection.Dispose();
            }
            catch
            {
                // ignored
            }

            _disposed = true;
        }
    }
}
