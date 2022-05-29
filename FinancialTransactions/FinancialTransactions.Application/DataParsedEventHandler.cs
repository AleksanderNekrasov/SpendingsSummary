using SpendingSummary.Common.Interfaces;
using SpendingSummary.Common.Models;

namespace SpendingSummary.FinancialTransactions.Application
{
    public class DataParsedEventHandler : IQueueEventHandler<DataParsedEvent>
    {
        public Task HandleQueueEventAsync(DataParsedEvent queueEvent)
        {
            throw new System.NotImplementedException();
        }        
    }
}