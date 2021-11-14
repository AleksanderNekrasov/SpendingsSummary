using System.Threading.Tasks;

namespace SpendingSummary.Common.Interfaces
{
    public interface IQueuePublisher : IQueueMessageBus
    {
        Task PublishAsync(IQueueEvent queueEvent);
    }
}