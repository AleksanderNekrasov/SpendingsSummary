using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SpendingSummary.Common.ApiCommons;
using SpendingSummary.Common.Interfaces;
using SpendingSummary.Common.Models;
using SpendingSummary.DataStore.WorkerService;
using SpendingSummary.FinancialTransactions.Application;
using SpendingSummary.QueueBus;
using static SpendingSummary.Common.EnvFile;

SetEnvironmentalVariablesFromEnvFile();
var builder = WebApplication.CreateBuilder(args);
builder.Configuration
    .AddEventDefinitionConfigFile()
    .AddEnvironmentVariables();
builder.Services
    .AddQueueConnection(builder.Configuration)
    .AddScoped<IQueueEventHandler<DataParsedEvent>, DataParsedEventHandler>()
    .AddHostedService<QueueSubscriptionHostedService>();

WebApplication app = builder.Build();
app.Run();