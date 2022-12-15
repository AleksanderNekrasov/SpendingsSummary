namespace SpendingSummary.FinancialTransactions.Core.ValueObjects
{
    public sealed record Money
    {
        public decimal Amount { get; init; }

        public Currency Currency { get; init; }

        public Money(decimal amount, Currency currency)
        {
            Amount = amount;
            Currency = currency;
        }

        public Money Add(Money amount) => new (Math.Round(this.Amount + amount.Amount, 2), this.Currency);

        public override string ToString() => string.Format($"{this.Amount} {this.Currency}");
    }
}
