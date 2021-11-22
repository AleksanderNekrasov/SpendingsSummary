using SpendingSummary.Common.Interfaces;
using SpendingSummary.Common.Models;
using System.Threading.Tasks;

namespace SpendingSummary.DataStore.WorkerService
{
    public class DataParsedEventHandler : IQueueEventHandler<DataParsedEvent>
    {
        public Task HandleAsync(DataParsedEvent queueEvent)
        {
            return Task.CompletedTask;
            //throw new NotImplementedException();
        }
    }
}
