using SpendingsSummary.Model;
using System;
using System.Collections.Generic;

namespace SpendingSummary.Common.Interfaces
{
    public interface IQueueEvent
    {
        Guid EventId { get; }

        IEnumerable<TransactionModel> Transactions { get; }
    }
}
