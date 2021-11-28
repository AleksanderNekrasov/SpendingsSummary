using ImportedReports.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpendingsSummary.Interfaces
{
    public interface ITransactionsParser
    {
        Task<IEnumerable<TransactionModel>> ParseTransactionFromString(IEnumerable<string> transactions);
    }
}