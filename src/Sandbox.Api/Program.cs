using System.Reflection;
using Sandbox.Api.Startup;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

Log.Information($"Initialising {Assembly.GetExecutingAssembly().GetName().Name} Service");

var builder = WebApplication.CreateBuilder(args);

builder.AddLogging()
    .AddDataServices()
    .AddApplicationServices()
    .AddSwagger()
    .AddWebServices();

var app = builder.Build();

app.UseConfiguredSwagger();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();