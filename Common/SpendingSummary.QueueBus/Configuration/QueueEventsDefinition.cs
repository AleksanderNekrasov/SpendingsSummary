using System.Collections.Generic;

namespace SpendingSummary.QueueBus.Configuration
{
    public class QueueEventsDefinition
    {
        public IDictionary<string, QueueEvent> Events { get; set; }
    }
}
