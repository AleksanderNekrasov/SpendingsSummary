using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SpendingsSummary.WorkerService.IoC;
using Microsoft.EntityFrameworkCore;

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
                        .RegisterDataDependancy(hostContext.Configuration)
                        .RegisterApplicationDependancy(hostContext.Configuration)
                        .AddHostedService<ReadFromQueueService>()
                        .AddDbContext<DbContext>(
                            options => options.UseSqlServer("name=ConnectionStrings:DefaultConnection"));
                });
    }
}
