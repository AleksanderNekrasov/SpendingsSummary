using SpendingSummary.FinancialTransactions.Core.Account;
using SpendingSummary.FinancialTransactions.Core.Enums;
using SpendingSummary.FinancialTransactions.Core.TransactionParty;
using SpendingSummary.FinancialTransactions.Core.ValueObjects;

namespace SpendingSummary.FinancialTransactions.Core.FinancialTransaction
{
    public interface ITransaction
    {
        AccountId AccountId { get; init; }
        Money Amount { get; init; }
        Task<IAccount> GetAccountAsync { get; }
        TransactionId Id { get; init; }
        AccountId TargetAccountId { get; init; }
        DateTime Time { get; init; }
        TransactionPartyId TransactionPartyId { get; init; }
        TransactionType Type { get; init; }

        Task<ITransactionParty> GetTransactionPartyAsync();
    }
}