using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System;
using Polly;
using System.Threading.Tasks;
using SpendingSummary.Common.QueueBus.Interfaces;
using SpendingSummary.QueueBus.Configuration;

namespace SpendingSummary.Common.QueueBus
{
    public class QueueConnection : IQueueConnection
    {
        private readonly object _lock = new object();
        private readonly ILogger<QueueConnection> _logger;
        private IConnection _connection;
        private bool _disposed;
        private ConnectionFactory _connectionFactory;

        public QueueConnection(IOptions<QueueConfigurations> options, ILogger<QueueConnection> logger)
        {
            _logger = logger;
            _connectionFactory = new ConnectionFactory
            {
                HostName = options.Value.Host,
                Port = options.Value.Port,
                VirtualHost = "/"
            };

            if (!string.IsNullOrEmpty(options.Value?.Username))
            {
                _connectionFactory.UserName = options.Value.Username;
            }

            if (!string.IsNullOrEmpty(options.Value?.Password))
            {
                _connectionFactory.Password = options.Value.Password;
            }
        }

        public IModel CreateModel()
        {
            return _connection.CreateModel();
        }

        public bool IsOpen => _connection != null && _connection.IsOpen && !_disposed;

        public async Task<bool> TryConnectAsync()
        {
            var policy = Policy
              .Handle<BrokerUnreachableException>()
              .WaitAndRetryForeverAsync(
                retryAttempt => TimeSpan.FromSeconds(1 + retryAttempt),
                (exception, timespan) =>
                {
                    _logger.LogInformation("Queue is not yet ready");
                });

            await policy.ExecuteAsync(() => Connect());
            return true;
        }

        private async Task<bool> Connect()
        {
            //there is no async methods in ConnectionFactory
            //and also no callback for when connection is opened
            //thus using Task.Yield() and will think how to make it async in future

            await Task.Yield(); 
            _connection = _connectionFactory.CreateConnection();
            if (!IsOpen) return false;

            _connection.ConnectionShutdown += OnConnectionShutdown;
            _connection.CallbackException += OnCallbackException;
            _connection.ConnectionBlocked += OnConnectionBlocked;
            return true;
        }

        private void OnConnectionBlocked(object sender, ConnectionBlockedEventArgs e)
        {
            //TODO: Logging
        }

        private void OnCallbackException(object sender, CallbackExceptionEventArgs e)
        {
            //TODO: Logging
        }

        private void OnConnectionShutdown(object sender, ShutdownEventArgs reason)
        {
            if (_disposed)
            {
                return;
            }

            //TODO: Logging
        }

        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }

            _disposed = true;

            try
            {
                _connection.Dispose();
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}
