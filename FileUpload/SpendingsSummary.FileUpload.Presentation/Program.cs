using SpendingsSummary.FileUpload.Presentation.IoC;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegisterFileUpload();
builder.Services.AddMvc();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=FileUpload}/{action=Index}/{id?}");

app.Run();
