namespace SpendingSummary.Common.Interfaces
{
    public interface IQueueMessageBus
    {
        void BindQueue<T>() where T : IQueueEvent;
    }
}