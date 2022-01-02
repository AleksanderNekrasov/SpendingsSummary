using SpendingsSummary.FileUpload.Presentation.IoC;
using SpendingSummary.Common.ApiCommons;
using SpendingSummary.Common.QueueBus;
using SpendingSummary.Common.QueueBus.Interfaces;
using SpendingSummary.QueueBus;
using SpendingSummary.QueueBus.Configuration;
using static SpendingSummary.Common.EnvFile;

SetEnvironmentalVariablesFromEnvFile();
var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddEventDefinitionConfigFile();
builder.Services.Configure<QueueEventsDefinition>(builder.Configuration);
builder.Services.RegisterFileUpload();
builder.Services.AddMvc();
builder.Services.AddSingleton<IQueueConnection, QueueConnection>();
builder.Services.AddHostedService<QueueInitializer>();
var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=FileUpload}/{action=Index}/{id?}");

app.Run();
