using Microsoft.Extensions.DependencyInjection;
using SpendingSummary.Common.Interfaces;
using SpendingSummary.Queue.Interfaces;

namespace SpendingSummary.Queue
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
