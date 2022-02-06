using MediatR;
using SpendingsSummary.FileUpload.Application.Commands;
using SpendingsSummary.FileUpload.Application.Interfaces;
using SpendingsSummary.FileUpload.DAL;
using SpendingSummary.QueueBus;

namespace SpendingsSummary.FileUpload.Presentation.IoC;

public static class RegisterFileUploadDependencies
{
    public static IServiceCollection RegisterFileUpload(this IServiceCollection services) 
    {
        return services
            .AddMediatR(typeof(SaveFileCommand).Assembly, typeof(PublishEventCommand).Assembly)
            .AddTransient<IFileRepository,FileRepository>();
    }
}
