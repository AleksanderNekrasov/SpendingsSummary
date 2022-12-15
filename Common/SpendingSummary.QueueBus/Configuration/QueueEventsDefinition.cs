using System.Collections.Generic;

namespace SpendingSummary.QueueBus.Configuration
{
    public sealed class QueueEventsDefinition
    {
        public IDictionary<string, QueueEvent> Events { get; set; }
    }
}
