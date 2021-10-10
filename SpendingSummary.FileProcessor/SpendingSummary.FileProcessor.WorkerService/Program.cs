using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SpendingsSummary.Application;
using SpendingsSummary.WorkerService.IoC;
using SpendingSummary.Queue;

namespace SpendingsSummary.WorkerService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services
                        .Configure<QueueConfigurations>(hostContext.Configuration.GetSection("RaddisQueueSettings"))
                        .Configure<ImportSettings>(hostContext.Configuration.GetSection("ImportSettings"))
                        .RegisterDataDependancy()
                        .RegisterApplicationDependancy(hostContext.Configuration)
                        .AddQueueConnection()
                        .AddHostedService<ReadFromQueueService>();
                });
    }
}
