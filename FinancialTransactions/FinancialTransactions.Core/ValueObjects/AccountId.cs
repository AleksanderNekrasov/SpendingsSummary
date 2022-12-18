namespace SpendingSummary.FinancialTransactions.Core.ValueObjects
{
    public sealed record AccountId
    {
        public Guid? Id { get; init; }

        public string? Number { get; init; }

        public bool IsEmpty { get; init; }

        public static AccountId Init(Guid id, string number) => new AccountId(id, number);

        public static AccountId Empty() => new AccountId();

        private AccountId() => IsEmpty = true;

        private AccountId(Guid id, string number) 
        {
            Id = id;
            Number = number;
        }
    } 
}
