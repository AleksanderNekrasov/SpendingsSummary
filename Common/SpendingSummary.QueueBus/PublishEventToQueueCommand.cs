using SpendingSummary.Common.Interfaces;
using MediatR;

namespace SpendingSummary.QueueBus
{
    public sealed class PublishEventToQueueCommand : IRequest
    {
        public PublishEventToQueueCommand(IQueueEvent eventToPublish)
        {
            Event = eventToPublish;
        }

        public IQueueEvent Event { get; }
    }
}
