using RabbitMQ.Client;
using System;
using System.Threading.Tasks;

namespace SpendingSummary.QueueBus.Interfaces
{
    public interface IQueueChannels : IDisposable
    {
        Task<IModel> CreateAsync();
    }
}