namespace SpendingSummary.FinancialTransactions.DataAccess.Entities
{
    public class TransactionEntity
    {
        public int Id { get; set; }

        public string ReferenceNumber { get; set; }

        public decimal Ammount { get; set; }

        public DateTime Time { get; init; }

        public int Type { get; init; }

        public AccountEntity Account { get; set; }

        public AccountEntity TargetAccount { get; set; }

        public TransactionPartyEntity Party { get; set; }
    }
}
