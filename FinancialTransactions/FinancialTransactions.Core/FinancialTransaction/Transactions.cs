using System.Collections;

namespace SpendingSummary.FinancialTransactions.Core.FinancialTransaction
{
    public sealed class Transactions : ITransactions
    {
        private readonly IEnumerable<Transaction> transactions;

        public static ITransactions Init(IEnumerable<Transaction> transactions) =>
            new Transactions(transactions);

        public decimal TotalAmmount() =>
            transactions.Sum(x => x.Amount?.Amount ?? 0);

        public DateTime FirstTransactionTime() =>
            transactions.Min(x => x.Time);

        public DateTime LastTransactionTime() =>
            transactions.Max(x => x.Time);

        public int Count() =>
            transactions.Count();

        public IEnumerator<Transaction> GetEnumerator() =>
            transactions.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() =>
            transactions.GetEnumerator();

        private Transactions(IEnumerable<Transaction> transactions) =>
            this.transactions = transactions;
    }
}
