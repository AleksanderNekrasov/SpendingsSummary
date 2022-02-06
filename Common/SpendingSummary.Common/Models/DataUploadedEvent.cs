using SpendingSummary.Common.Interfaces;

namespace SpendingSummary.Common.Models
{
    public class DataUploadedEvent : IQueueEvent
    {
        public string FileName { get; set; }
    }
}
