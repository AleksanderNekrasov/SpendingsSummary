using SpendingSummary.Common.Models;
using System.Collections.Generic;

namespace SpendingsSummary.ReportParser.Pekao
{
    internal static class PekaoTransactionTypeMap
    {
        internal static Dictionary<string, TransactionType> Types =
            new Dictionary<string, TransactionType>
            {
                ["ZAKUP"] = TransactionType.Buying,
                ["PŁATNOŚĆ PEOPAY"] = TransactionType.Buying,
                ["PRZELEW INTERNET M/B"] = TransactionType.Transfer,
                ["PRZELEW KRAJOWY MIĘDZYBANKOWY"] = TransactionType.Transfer,
                ["PRZELEW MOBILE"] = TransactionType.Transfer,
                ["SPŁATA KREDYTU"] = TransactionType.KreditPayment,
                ["WPŁATA NA RACHUNEK KARTY"] = TransactionType.KreditCardPayment,
                ["WYPŁATA KARTĄ"] = TransactionType.WithdrawalATM
            };
    }
}
