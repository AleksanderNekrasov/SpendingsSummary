using SpendingSummary.Common.Interfaces;
using System;
using System.Collections.Generic;

namespace SpendingSummary.Common.Models
{
    public sealed class DataParsedEvent : IQueueEvent
    {
        public Guid EventId { get; set; }

        public IEnumerable<TransactionDto> Transactions { get; set; }
    }
}
