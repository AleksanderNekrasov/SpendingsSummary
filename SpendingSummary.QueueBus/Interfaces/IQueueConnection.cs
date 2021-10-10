using RabbitMQ.Client;
using System;

namespace SpendingSummary.Queue.Interfaces
{
    public interface IQueueConnection : IDisposable
    {
        IModel CreateModel();

        bool TryConnect();

        bool IsOpen { get; }
    }
}