using SpendingSummary.FinancialTransactions.Core.Enums;
using SpendingSummary.FinancialTransactions.Core.FinancialTransaction;
using SpendingSummary.FinancialTransactions.Core.ValueObjects;

namespace SpendingSummary.FinancialTransactions.Core.TransactionParty
{
    public interface ITransactionParty
    {
        TransactionPartyCategory Category { get; }

        TransactionPartyId Id { get; init; }

        Task<ITransactions> GetTransactionsAsync();

        Task SetCategoryAsync(TransactionPartyCategory category);
    }
}