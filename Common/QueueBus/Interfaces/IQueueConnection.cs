using RabbitMQ.Client;
using System;
using System.Threading.Tasks;

namespace SpendingSummary.Common.QueueBus.Interfaces
{
    public interface IQueueConnection : IDisposable
    {
        IModel CreateModel();

        Task<bool> TryConnectAsync();

        bool IsOpen { get; }
    }
}