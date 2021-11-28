using Microsoft.Extensions.DependencyInjection;
using SpendingSummary.Common.Interfaces;
using SpendingSummary.Common.QueueBus.Interfaces;

namespace SpendingSummary.Common.QueueBus
{
    public static class RegisterSpendingSummaryQueue
    {
        public static IServiceCollection AddQueueConnection(this IServiceCollection services)
        {
            services.AddSingleton<IQueueConnection, QueueConnection>();
            services.AddTransient<IQueueSubscriber, QueueSubscriber>();
            services.AddTransient<IQueuePublisher, QueuePublisher>();
            return services;
        }
    }
}
