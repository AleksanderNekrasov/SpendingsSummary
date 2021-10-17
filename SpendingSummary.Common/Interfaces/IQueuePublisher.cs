namespace SpendingSummary.Common.Interfaces
{
    public interface IQueuePublisher : IQueueMessageBus
    {
        void Publish(IQueueEvent queueEvent);
    }
}