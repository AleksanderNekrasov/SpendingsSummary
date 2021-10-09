using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SpendingSummary.Queue.Interfaces;
using System;

namespace SpendingSummary.Queue
{
    public class QueueConnection : IQueueConnection
    {
        private readonly object _lock = new object();
        private IConnection _connection;
        private bool _disposed;
        private ConnectionFactory _connectionFactory;

        public QueueConnection(QueueConfigurations options)
        {
            _connectionFactory = new ConnectionFactory
            {
                HostName = options.Host,
                DispatchConsumersAsync = options.DispatchConsumersAsync
            };

            if (!string.IsNullOrEmpty(options.VirtualHost))
            {
                _connectionFactory.VirtualHost = options.VirtualHost;
            }

            if (!string.IsNullOrEmpty(options.Username))
            {
                _connectionFactory.UserName = options.Username;
            }

            if (!string.IsNullOrEmpty(options.Password))
            {
                _connectionFactory.Password = options.Password;
            }
        }

        public IModel CreateModel()
        {
            return _connection.CreateModel();
        }

        public bool IsOpen => _connection != null && _connection.IsOpen && !_disposed;

        public bool TryConnect()
        {
            lock (_lock)
            {
                _connection = _connectionFactory.CreateConnection();

                if (!IsOpen)
                {
                    return false;
                }

                _connection.ConnectionShutdown += OnConnectionShutdown;
                _connection.CallbackException += OnCallbackException;
                _connection.ConnectionBlocked += OnConnectionBlocked;
                return true;
            }
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
