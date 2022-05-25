using System;

namespace ImportedReports.Model
{
    public sealed class TransactionModel
    {
        public DateTime BookingDate { get; set; }

        public DateTime CurrencyDate { get; set; }

        public string SenderOrRecipientName { get; set; }

        public string SourceAccount { get; set; }

        public string TargetAccount { get; set; }

        public string Title { get; set; }

        public decimal? Ammount { get; set; }

        public string Currency { get; set; }

        public string ReferenceType { get; set; }

        public TransactionType Type { get; set; }
    }
}
