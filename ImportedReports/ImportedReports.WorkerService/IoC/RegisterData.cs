using ImportedReports.Parser.ReportParser.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using SpendingsSummary.ReportParser.Pekao;
using SpendingsSummary.ReportReader;

namespace SpendingsSummary.WorkerService.IoC
{
    public static class RegisterData
    {
        public static IServiceCollection RegisterDataDependancy(this IServiceCollection services)
        {
            return services
                .AddTransient<IReportLinesRepository, ReportLinesRepository>()
                .AddTransient<ITransactionsParser, PekaoTransactionsParser>(); //TODO: feature
        }
    }
}
