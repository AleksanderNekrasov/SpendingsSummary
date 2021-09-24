using System;
using System.Globalization;

namespace SpendingsSummary.ReportParser
{
    internal static class ParseHelper
    {
        internal static DateTime ToDateTime(this string dateAsString, string format, CultureInfo culture)
            => DateTime.ParseExact(dateAsString, format, culture.DateTimeFormat);

        internal static decimal? ToDecimal(this string valueAsString, IFormatProvider provider)
        {
            if (string.IsNullOrWhiteSpace(valueAsString)) return null;

            return decimal.Parse(valueAsString, NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowLeadingSign, provider);
        }
    }
}
