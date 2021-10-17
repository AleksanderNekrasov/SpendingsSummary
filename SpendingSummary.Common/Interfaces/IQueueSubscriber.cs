using System;

namespace SpendingSummary.Common.Interfaces
{
    public interface IQueueSubscriber :  IQueueMessageBus, IDisposable 
    {
        void Subscribe<T>() where T : IQueueEvent;

        void UnsubscribeAll();
    }
}