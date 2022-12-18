using SpendingSummary.FinancialTransactions.Core.ValueObjects;

namespace SpendingSummary.FinancialTransactions.Core.FinancialTransaction
{
    public interface ITransactionRepository
    {
        Task<ITransactions> GetTransactionsByPartyIdAsync(TransactionPartyId partyId);

        Task<ITransactions> GetTransactionsByAccountIdAsync(AccountId partyId);
    }
}
