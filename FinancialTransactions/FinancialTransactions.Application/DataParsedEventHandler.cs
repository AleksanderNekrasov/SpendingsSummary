using SpendingSummary.Common.Interfaces;
using SpendingSummary.Common.Models;

namespace SpendingSummary.FinancialTransactions.Application
{
    public sealed class DataParsedEventHandler : IQueueEventHandler<DataParsedEvent>
    {
        public Task HandleQueueEventAsync(DataParsedEvent queueEvent)
        {
            throw new System.NotImplementedException();
        }        
    }
}