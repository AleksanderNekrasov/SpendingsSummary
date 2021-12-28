using ImportedReports.Model;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ImportedReports.Parser.ReportParser.Interfaces
{
    public interface ITransactionsParser
    {
        Task<IEnumerable<TransactionModel>> ParseTransactionFromString(IEnumerable<string> transactions, CancellationToken cancellationToken);
    }
}