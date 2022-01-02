using System;

namespace SpendingSummary.QueueBus
{
    public class QueueEventNotConfiguredException : Exception
    {
        public QueueEventNotConfiguredException(string eventName) : base($"Event {eventName} is not configured in queue-events-definition.yaml")
        {
        }
    }
}
