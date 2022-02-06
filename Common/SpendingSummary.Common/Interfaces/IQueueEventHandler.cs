using System.Threading;
using System.Threading.Tasks;

namespace SpendingSummary.Common.Interfaces
{
    public interface IQueueEventHandler<T> where T: IQueueEvent
    {
        Task HandleQueueEventAsunc(T queueEvent);
    }
}
