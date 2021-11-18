using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SpendingsSummary.Application;
using SpendingsSummary.WorkerService.IoC;
using SpendingSummary.Common;
using SpendingSummary.Queue;
using System;
using System.Linq;

namespace SpendingsSummary.WorkerService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            SetEnvironmentalVariablesFromEnvFile();
            var variables = Environment.GetEnvironmentVariables();
            
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
                        .AddQueueConnection()
                        .AddHostedService<ReadFromQueueService>();
                });

        private static void SetEnvironmentalVariablesFromEnvFile() 
        {
            string envFilePath = ".env";
#if DEBUG
            envFilePath = "bin/Debug/net5.0/.env";
#endif
            EnvFile.Read($"{Environment.CurrentDirectory}/{envFilePath}").ToList().ForEach(x =>
            {
                Environment.SetEnvironmentVariable(x.key, x.value);
            });
        }
    }
}
