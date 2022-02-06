using ImportedReports.Model;
using System.Collections.Generic;

namespace ImportedReports.Parser.ReportParser.Interfaces
{
    public interface ITransactionsParser
    {
        IEnumerable<TransactionModel> ParseTransactionFromString(IEnumerable<string> transactions);
    }
}