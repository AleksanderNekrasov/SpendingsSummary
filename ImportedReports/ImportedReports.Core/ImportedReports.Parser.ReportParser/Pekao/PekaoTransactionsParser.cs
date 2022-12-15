using ImportedReports.Model;
using ImportedReports.Parser.ReportParser.Interfaces;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using static SpendingsSummary.ReportParser.Pekao.PekaoTransactionPropertyPosition;
using static SpendingsSummary.ReportParser.Pekao.PekaoTransactionTypeMap;

namespace SpendingsSummary.ReportParser.Pekao
{
    public sealed class PekaoTransactionsParser : ITransactionsParser
    {
        //private static string RegexToSplitConfig = $"\\W{{2,}}|\\br\\\\n\\b|[,]";
        private static string SplitSeparator = ";(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)";
        private static string DateFormat = "dd.MM.yyyy";
        private static CultureInfo _reportCulture = new CultureInfo("pl-PL");

        public IEnumerable<TransactionModel> ParseTransactionFromString(IEnumerable<string> transactions)
            => transactions.Where(x => !string.IsNullOrEmpty(x))
            .Select(Parse);

        private static TransactionModel Parse(string transactionLine)
        {
            var transaction = Regex.Split(transactionLine, SplitSeparator);
            return new TransactionModel
            {
                BookingDate = transaction[(int)BookingDate].ToDateTime(DateFormat, _reportCulture),
                CurrencyDate = transaction[(int)CurrencyDate].ToDateTime(DateFormat, _reportCulture),
                SenderOrRecipientName = transaction[(int)SenderOrRecipient],
                TargetAccount = transaction[(int)TargetAccount],
                SourceAccount = transaction[(int)SourceAccount],
                Title = transaction[(int)Title],
                Ammount = transaction[(int)Ammount].ToDecimal(_reportCulture),
                Currency = transaction[(int)Currency],
                ReferenceType = transaction[(int)ReferenceType],
                Type = ParseType(transaction[(int)TypeOfTransaction])
            };
        }

        private static TransactionType ParseType(string transactionType)
        {
            Types.TryGetValue(transactionType, out var type);
            return type;
        }
    }
}
