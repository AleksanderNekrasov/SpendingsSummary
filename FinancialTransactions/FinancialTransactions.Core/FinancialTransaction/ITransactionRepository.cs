namespace SpendingSummary.FinancialTransactions.Core.FinancialTransaction
{
    public interface ITransactionRepository
    {
        ITransactions GetTransactionsByPartyId(Guid partyId);
    }
}
