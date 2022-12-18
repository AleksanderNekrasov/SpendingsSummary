using SpendingSummary.FinancialTransactions.Core.Enums;
using SpendingSummary.FinancialTransactions.Core.FinancialTransaction;
using SpendingSummary.FinancialTransactions.Core.ValueObjects;

namespace SpendingSummary.FinancialTransactions.Core.TransactionParty
{
    public sealed class TransactionParty : ITransactionParty
    {
        private readonly ITransactionRepository _transRepo;

        private readonly ITransactionPartyRepository _repository;

        public TransactionPartyId Id { get; init; }

        public TransactionPartyCategory Category { get; private set; }

        public static ITransactionParty Init(TransactionPartyId id,
            TransactionPartyCategory category, 
            ITransactionRepository transRepo,
            ITransactionPartyRepository repo) =>
            new TransactionParty (id, category, transRepo, repo);

        public static async Task<ITransactionParty> InitAsync(TransactionPartyId id,
            ITransactionRepository transRepo,
            ITransactionPartyRepository repo) =>
            await repo.GetAsync(id);

        public async Task SetCategoryAsync(TransactionPartyCategory category)
        {
            Category = category;
            await _repository.UpdateAsync(this);
        }

        public async Task<ITransactions> GetTransactionsAsync () => 
            await _transRepo.GetTransactionsByPartyIdAsync(Id);

        private TransactionParty(
            TransactionPartyId id,
            TransactionPartyCategory category,
            ITransactionRepository transRepo,
            ITransactionPartyRepository repository)
        {
            Id = id;
            Category = category;
            _transRepo = transRepo;
            _repository = repository;
        }
    }
}
