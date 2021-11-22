using SpendingsSummary.Model;
using SpendingSummary.Common.Interfaces;
using System;
using System.Collections.Generic;

namespace SpendingSummary.Common.Models
{
    public class DataParsedEvent : IQueueEvent
    {
        public Guid EventId { get; set; }

        public IEnumerable<TransactionModel> Transactions { get; set; }
    }
}
