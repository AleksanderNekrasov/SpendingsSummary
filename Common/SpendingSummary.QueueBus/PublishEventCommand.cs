using SpendingSummary.Common.Interfaces;
using MediatR;

namespace SpendingSummary.QueueBus
{
    public class PublishEventCommand : IRequest
    {
        public PublishEventCommand(IQueueEvent eventToPublish)
        {
            Event = eventToPublish;
        }

        public IQueueEvent Event { get; }
    }
}
