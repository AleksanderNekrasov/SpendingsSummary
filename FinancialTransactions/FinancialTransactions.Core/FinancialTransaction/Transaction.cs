using SpendingSummary.FinancialTransactions.Core.Enums;
using SpendingSummary.FinancialTransactions.Core.ValueObjects;

namespace SpendingSummary.FinancialTransactions.Core.FinancialTransaction
{
    public sealed class Transaction
    {
        public TransactionId Id { get; init; }

        public Money Amount { get; init; }

        public DateTime Time { get; init; }

        public TransactionType Type { get; init; }
    }
}
