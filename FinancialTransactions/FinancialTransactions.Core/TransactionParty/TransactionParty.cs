using SpendingSummary.FinancialTransactions.Core.Enums;
using SpendingSummary.FinancialTransactions.Core.FinancialTransaction;

namespace SpendingSummary.FinancialTransactions.Core.TransactionParty
{
    public sealed class TransactionParty
    {
        public Guid Id { get; init; }

        public string Name { get; init; }

        public TransactionPartyCategory Category { get; private set; }

        public TransactionParty(Guid id, string name, TransactionPartyCategory category)
        {
            Id = id;
            Name = name;
            Category = category;
        }
    }
}
