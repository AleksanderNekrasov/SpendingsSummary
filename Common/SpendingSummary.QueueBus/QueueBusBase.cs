using Microsoft.Extensions.Options;
using SpendingSummary.Common.Interfaces;
using SpendingSummary.QueueBus.Configuration;
using System.Collections.Generic;

namespace SpendingSummary.QueueBus
{
    public abstract class QueueBusBase
    {
        private readonly IDictionary<string, QueueEvent> _events;

        protected QueueBusBase(IOptions<QueueEventsDefinition> options)
        {
            _events = options.Value.Events;
        }

        protected QueueEvent GetEventByType<T>(T queueEvent) where T : IQueueEvent
        {
            return GetEventByType<T>();
        }

        protected QueueEvent GetEventByType<T>() where T : IQueueEvent
        {
            var eventName = typeof(T).Name;
            if (_events.TryGetValue(eventName, out QueueEvent ev)) return ev;
            throw new QueueEventNotConfiguredException(eventName);
        }
    }
}
