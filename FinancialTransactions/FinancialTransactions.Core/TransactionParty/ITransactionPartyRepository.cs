using SpendingSummary.FinancialTransactions.Core.ValueObjects;

namespace SpendingSummary.FinancialTransactions.Core.TransactionParty
{
    public interface ITransactionPartyRepository
    {
        Task AddAsync(ITransactionParty party);

        Task UpdateAsync(ITransactionParty party);

        Task<ITransactionParty> GetAsync(TransactionPartyId id);
    }
}
