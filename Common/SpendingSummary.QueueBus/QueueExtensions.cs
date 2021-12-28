using Microsoft.Extensions.DependencyInjection;
using SpendingSummary.Common.Interfaces;
using SpendingSummary.Common.QueueBus;
using SpendingSummary.Common.QueueBus.Interfaces;

namespace SpendingSummary.QueueBus
{
    public static class QueueExtensions
    {
        public static IServiceCollection AddQueueConnection(this IServiceCollection services)
        {
            services.AddSingleton<IQueueConnection, QueueConnection>();
            services.AddTransient<IQueueSubscriber, QueueSubscriber>();
            return services;
        }
    }
}
