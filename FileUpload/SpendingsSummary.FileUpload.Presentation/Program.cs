using SpendingsSummary.Application;
using SpendingsSummary.FileUpload.Presentation.IoC;
using SpendingSummary.Common.ApiCommons;
using SpendingSummary.QueueBus;
using static SpendingSummary.Common.EnvFile;

SetEnvironmentalVariablesFromEnvFile();
var builder = WebApplication.CreateBuilder(args);
builder.Configuration
    .AddEventDefinitionConfigFile()
    .AddEnvironmentVariables();
builder.Services
    .Configure<ImportSettings>(builder.Configuration.GetSection("ImportSettings"))
    .RegisterFileUpload()
    .AddQueueConnection(builder.Configuration)
    .AddMvc();
var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=FileUpload}/{action=Index}/{id?}");

app.Run();
