using ImportedReports.Application.Mapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SpendingsSummary.Application;
using SpendingSummary.Common.Interfaces;
using SpendingSummary.Common.Models;
using SpendingSummary.QueueBus;

namespace SpendingsSummary.WorkerService.IoC
{
    public static class RegisterApplication
    {
        public static IServiceCollection RegisterApplicationDependancy(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .Configure<ImportSettings>(configuration.GetSection("ImportSettings"))
                .AddMediatR(typeof(PublishEventToQueueCommand))
                .AddAutoMapper(typeof(TransactionModelProfile))
                .AddScoped<IQueueEventHandler<DataUploadedEvent>, DataUploadedHandler>();
        }
    }
}
