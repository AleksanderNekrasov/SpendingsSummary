using SpendingSummary.Common.Models;
using System;
using System.Collections.Generic;

namespace SpendingSummary.Common.Interfaces
{
    public interface IQueueEvent
    {
        Guid EventId { get; }

        IEnumerable<TransactionDto> Transactions { get; }
    }
}
