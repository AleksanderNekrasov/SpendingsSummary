namespace SpendingSummary.FinancialTransactions.DataAccess.Entities
{
    public class TransactionPartyEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Category { get; set; }

        public IEnumerable<TransactionEntity> Transactions { get; set; }
    }
}
