using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SpendingSummary.Common.Interfaces;
using SpendingSummary.Common.Models;
using SpendingSummary.Queue;

namespace SpendingSummary.DataStore.WorkerService
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
                    services.Configure<QueueConfigurations>(hostContext.Configuration.GetSection("RaddisQueueSettings"))
                    .AddQueueConnection();
                    services.AddHostedService<Worker>();
                    services.AddTransient<IQueueEventHandler<DataParsedEvent>, DataParsedEventHandler>();
                });
    }
}
