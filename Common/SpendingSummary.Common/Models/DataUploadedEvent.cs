using SpendingSummary.Common.Interfaces;

namespace SpendingSummary.Common.Models
{
    public sealed class DataUploadedEvent : IQueueEvent
    {
        public string FileName { get; set; }
    }
}
