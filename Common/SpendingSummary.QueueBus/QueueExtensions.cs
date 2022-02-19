using Microsoft.Extensions.DependencyInjection;
using SpendingSummary.Common.Interfaces;
using SpendingSummary.Common.QueueBus;
using SpendingSummary.Common.QueueBus.Interfaces;
using SpendingSummary.QueueBus.Interfaces;

namespace SpendingSummary.QueueBus
{
    public static class QueueExtensions
    {
        public static IServiceCollection AddQueueConnection(this IServiceCollection services)
        {
            return services.AddSingleton<IQueueConnection, QueueConnection>()
                .AddSingleton<IQueueChannels, QueueChannels>()
                .AddTransient<IQueueSubscriber, QueueSubscriber>()
                .AddHostedService<QueueInitializer>();
        }
    }
}
