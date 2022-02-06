using Microsoft.Extensions.Options;
using SpendingSummary.Common.Interfaces;
using SpendingSummary.QueueBus.Configuration;
using System;
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
            return GetEventByType(queueEvent.GetType());
        }

        protected QueueEvent GetEventByType(Type eventType)
        {
            var eventName = eventType.Name;
            if (_events.TryGetValue(eventName, out QueueEvent ev)) return ev;
            throw new QueueEventNotConfiguredException(eventName);
        }
    }
}
