using SpendingsSummary.Application;
using SpendingsSummary.FileUpload.Presentation.IoC;
using SpendingSummary.Common.ApiCommons;
using SpendingSummary.Common.QueueBus;
using SpendingSummary.Common.QueueBus.Interfaces;
using SpendingSummary.QueueBus;
using SpendingSummary.QueueBus.Configuration;
using static SpendingSummary.Common.EnvFile;

SetEnvironmentalVariablesFromEnvFile();
var builder = WebApplication.CreateBuilder(args);
builder.Configuration
    .AddEventDefinitionConfigFile()
    .AddEnvironmentVariables();
builder.Services
    .Configure<QueueConfigurations>(builder.Configuration.GetSection("RaddisQueueSettings"))
    .Configure<ImportSettings>(builder.Configuration.GetSection("ImportSettings"))
    .Configure<QueueEventsDefinition>(builder.Configuration)
    .RegisterFileUpload()
    .AddSingleton<IQueueConnection, QueueConnection>()
    .AddHostedService<QueueInitializer>()
    .AddMvc();
var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=FileUpload}/{action=Index}/{id?}");

app.Run();
