using System.Threading.Tasks;

namespace SpendingSummary.Common.Interfaces
{
    public interface IQueueSubscriber
    {
        Task StartSubscribingAsync<T>() where T : IQueueEvent;
    }
}