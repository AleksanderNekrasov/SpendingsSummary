using SpendingSummary.Common.Interfaces;
using System;

namespace SpendingSummary.Common.Models
{
    public class DataParsedEvent : IQueueEvent
    {
        public Guid TransactionId { get; set; }
    }
}
