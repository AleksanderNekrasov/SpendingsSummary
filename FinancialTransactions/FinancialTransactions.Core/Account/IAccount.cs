using SpendingSummary.FinancialTransactions.Core.FinancialTransaction;
using SpendingSummary.FinancialTransactions.Core.ValueObjects;

namespace SpendingSummary.FinancialTransactions.Core.Account
{
    public interface IAccount
    {
        AccountId Id { get; init; }

        Task<ITransactions> GetTransactionsAsync();

        Task<decimal> GetTransactionBalance();
    }
}