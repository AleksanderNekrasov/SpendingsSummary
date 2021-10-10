namespace SpendingSummary.Common.Interfaces
{
    public interface IQueuePublisher
    {
        void Publish(IQueueEvent queueEvent);
    }
}