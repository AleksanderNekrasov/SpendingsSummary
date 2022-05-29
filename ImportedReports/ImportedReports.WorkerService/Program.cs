using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SpendingsSummary.Application;
using SpendingsSummary.WorkerService.IoC;
using SpendingSummary.Common.ApiCommons;
using SpendingSummary.QueueBus;
using SpendingSummary.QueueBus.Configuration;
using SpendingSummary.Common.Models;
using static SpendingSummary.Common.EnvFile;

namespace SpendingsSummary.WorkerService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            SetEnvironmentalVariablesFromEnvFile();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(x => x
                    .AddJsonFile("appsettings.json", true)
                    .AddEventDefinitionConfigFile() 
                    .AddEnvironmentVariables()
                    .Build())
                .ConfigureServices((hostContext, services) =>
                {
                    services
                        .Configure<QueueConfigurations>(hostContext.Configuration.GetSection("RaddisQueueSettings"))
                        .Configure<ImportSettings>(hostContext.Configuration.GetSection("ImportSettings"))
                        .Configure<QueueEventsDefinition>(hostContext.Configuration)
                        .RegisterDataDependancy()
                        .RegisterApplicationDependancy(hostContext.Configuration)
                        .AddQueueConnection()
                        .AddHostedService<ReadFromQueueService>();
                });
    }
}
