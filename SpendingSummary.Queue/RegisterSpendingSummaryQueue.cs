using Microsoft.Extensions.DependencyInjection;
using SpendingSummary.Common.Interfaces;
using SpendingSummary.Queue.Interfaces;

namespace SpendingSummary.Queue
{
    public static class RegisterSpendingSummaryQueue
    {
        public static IServiceCollection AddQueueConnection(this IServiceCollection services,
            QueueConfigurations options)
        {
            services.AddSingleton<IQueueConnection>(sp =>
            {                
                return new QueueConnection(options);
            });

            services.AddTransient<IQueueSubscriber, QueueSubscriber>();
            return services;
        }
    }
}
