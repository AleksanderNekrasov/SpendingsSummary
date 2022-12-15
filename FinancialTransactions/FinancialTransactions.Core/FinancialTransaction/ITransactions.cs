namespace SpendingSummary.FinancialTransactions.Core.FinancialTransaction
{
    public interface ITransactions : IEnumerable<Transaction>
    {
        int Count();

        DateTime FirstTransactionTime();

        DateTime LastTransactionTime();

        decimal TotalAmmount();
    }
}