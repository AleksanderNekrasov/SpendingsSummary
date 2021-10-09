namespace SpendingSummary.Common.Interfaces
{
    public interface IQueueSubscriber
    {
        void Subscribe<T>(string queueName) where T : IQueueEvent;
    }
}