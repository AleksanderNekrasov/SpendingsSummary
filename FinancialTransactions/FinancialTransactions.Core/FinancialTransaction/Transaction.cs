using SpendingSummary.FinancialTransactions.Core.Account;
using SpendingSummary.FinancialTransactions.Core.Enums;
using SpendingSummary.FinancialTransactions.Core.TransactionParty;
using SpendingSummary.FinancialTransactions.Core.ValueObjects;

namespace SpendingSummary.FinancialTransactions.Core.FinancialTransaction
{
    public sealed class Transaction : ITransaction
    {
        private readonly ITransactionRepository _transRepo;

        private readonly ITransactionPartyRepository _transPartyRepo;

        private readonly IAccountRepository _accountRepository;

        public static ITransaction Init(ITransactionRepository transRepo,
            ITransactionPartyRepository transPartyRepo,
            IAccountRepository accountRepository,
            TransactionId id,
            Money amount,
            DateTime time,
            TransactionType type,
            TransactionPartyId transactionPartyId,
            AccountId accountId,
            AccountId targetAccountId) 
        {
            return new Transaction(transRepo,
            transPartyRepo,
            accountRepository,
            id,
            amount,
            time,
            type,
            transactionPartyId,
            accountId,
            targetAccountId);
        }

        public TransactionId Id { get; init; }

        public Money Amount { get; init; }

        public DateTime Time { get; init; }

        public TransactionType Type { get; init; }

        public TransactionPartyId TransactionPartyId { get; init; }

        public AccountId AccountId { get; init; }

        public AccountId TargetAccountId { get; init; }

        public Task<ITransactionParty> GetTransactionPartyAsync() =>
            _transPartyRepo.GetAsync(TransactionPartyId);

        public Task<IAccount> GetAccountAsync =>
            _accountRepository.GetAsync(AccountId);

        private Transaction(ITransactionRepository transRepo,
            ITransactionPartyRepository transPartyRepo,
            IAccountRepository accountRepository,
            TransactionId id,
            Money amount,
            DateTime time,
            TransactionType type,
            TransactionPartyId transactionPartyId,
            AccountId accountId,
            AccountId targetAccountId)
        {
            _transRepo = transRepo;
            _transPartyRepo = transPartyRepo;
            _accountRepository = accountRepository;
            Id = id;
            Amount = amount;
            Time = time;
            Type = type;
            TransactionPartyId = transactionPartyId;
            AccountId = accountId;
            TargetAccountId = targetAccountId;
        }
    }
}
