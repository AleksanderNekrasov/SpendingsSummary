using System.Threading.Tasks;

namespace SpendingSummary.Common.Interfaces
{
    public interface IQueueMessageBus
    {
        Task BindQueueAsync<T>() where T : IQueueEvent;
    }
}