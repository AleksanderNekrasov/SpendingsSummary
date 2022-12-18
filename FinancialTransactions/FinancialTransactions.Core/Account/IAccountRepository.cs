using SpendingSummary.FinancialTransactions.Core.ValueObjects;

namespace SpendingSummary.FinancialTransactions.Core.Account
{
    public interface IAccountRepository
    {
        Task AddAsync(IAccount account);

        Task UpdateAsync(IAccount account);

        Task<IAccount> GetAsync(AccountId id);
    }
}
