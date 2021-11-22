using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SpendingsSummary.Application;
using SpendingsSummary.Application.Interfaces;

namespace SpendingsSummary.WorkerService.IoC
{
    public static class RegisterApplication
    {
        public static IServiceCollection RegisterApplicationDependancy(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .Configure<ImportSettings>(configuration.GetSection("ImportSettings"))
                .AddTransient<IImportReportFromFile, ImportReportFromFileCase>();
        }
    }
}
