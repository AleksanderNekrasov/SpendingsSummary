using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SpendingSummary.Common.Interfaces;
using SpendingSummary.Common.QueueBus;
using SpendingSummary.Common.QueueBus.Interfaces;
using SpendingSummary.QueueBus.Configuration;
using SpendingSummary.QueueBus.Interfaces;
using System;

namespace SpendingSummary.QueueBus
{
    public static class QueueExtensions
    {
        public static IServiceCollection AddQueueConnection(this IServiceCollection services, IConfiguration config)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            return services
                .Configure<QueueConfigurations>(config.GetSection("RaddisQueueSettings"))
                .Configure<QueueEventsDefinition>(config)
                .AddSingleton<IQueueConnection, QueueConnection>()
                .AddSingleton<IQueueChannels, QueueChannels>()
                .AddTransient<IQueueSubscriber, QueueSubscriber>()
                .AddHostedService<QueueInitializer>();
        }
    }
}
