using MediatR;
using SpendingsSummary.FileUpload.Application.Commands;

namespace SpendingsSummary.FileUpload.Presentation.IoC;

public static class RegisterFileUploadDependencies
{
    public static IServiceCollection RegisterFileUpload(this IServiceCollection services) 
    {
        return services.AddMediatR(typeof(SaveFileCommand).Assembly);
    }
}
