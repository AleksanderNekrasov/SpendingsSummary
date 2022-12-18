using SpendingSummary.FinancialTransactions.Core.FinancialTransaction;
using SpendingSummary.FinancialTransactions.Core.ValueObjects;

namespace SpendingSummary.FinancialTransactions.Core.Account
{
    public class Account : IAccount
    {
        private readonly ITransactionRepository _transRepo;

        public AccountId Id { get; init; }

        public async Task<ITransactions> GetTransactionsAsync() =>
            await _transRepo.GetTransactionsByAccountIdAsync(Id);

        public async Task<decimal> GetTransactionBalance()
        {
            var transactions = await GetTransactionsAsync();
            return transactions.TotalAmmount();
        }

        private Account(ITransactionRepository transRepo, AccountId id)
        {
            _transRepo = transRepo;
            Id = id;
        }
    }
}
 