using System;

namespace SpendingSummary.QueueBus
{
    public sealed class QueueEventNotConfiguredException : Exception
    {
        public QueueEventNotConfiguredException(string eventName) : base($"Event {eventName} is not configured in queue-events-definition.yaml")
        {
        }
    }
}
