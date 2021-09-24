﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SpendingsSummary.ReportParser;
using SpendingsSummary.ReportParser.Pekao;
using SpendingsSummary.ReportReader;

namespace SpendingsSummary.WorkerService.IoC
{
    public static class RegisterData
    {
        public static IServiceCollection RegisterDataDependancy(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .AddTransient<IReportLinesRepository, ReportLinesRepository>()
                .AddTransient<ITransactionsParser, PekaoTransactionsParser>();
        }
    }
}