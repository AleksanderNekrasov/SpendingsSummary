using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SpendingsSummary.Application;
using SpendingsSummary.WorkerService.IoC;
using SpendingSummary.Common.QueueBus;
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
                .ConfigureAppConfiguration(x => x.AddJsonFile("appsettings.json", true).AddEnvironmentVariables().Build())
                .ConfigureServices((hostContext, services) =>
                {
                    services
                        .Configure<QueueConfigurations>(hostContext.Configuration.GetSection("RaddisQueueSettings"))
                        .Configure<ImportSettings>(hostContext.Configuration.GetSection("ImportSettings"))                        
                        .RegisterDataDependancy()
                        .RegisterApplicationDependancy(hostContext.Configuration)
                        .AddHostedService<QueueInitializer>()
                        .AddHostedService<ReadFromQueueService>();
                });
    }
}
