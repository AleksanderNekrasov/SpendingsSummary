namespace SpendingSummary.FinancialTransactions.DataAccess.Entities
{
    public class AccountEntity
    {
        public Guid AccountId { get; set; }

        public string Number { get; set; }

        public IEnumerable<TransactionEntity> Transactions { get; set; }
    }
}
