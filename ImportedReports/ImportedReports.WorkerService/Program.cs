using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SpendingsSummary.Application;
using SpendingsSummary.WorkerService.IoC;
using SpendingSummary.Common.ApiCommons;
using SpendingSummary.QueueBus;
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
                        .Configure<ImportSettings>(hostContext.Configuration.GetSection("ImportSettings"))
                        .RegisterDataDependancy()
                        .RegisterApplicationDependancy(hostContext.Configuration)
                        .AddQueueConnection(hostContext.Configuration)
                        .AddHostedService<QueueSubscriptionHostedService>();
                });
    }
}
