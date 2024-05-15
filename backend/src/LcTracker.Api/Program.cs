using System.Diagnostics;
using LcTracker.Api.Bootstrap;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    Log.Information("Environment: {0}", builder.Environment.EnvironmentName);

    builder.AddDependencies();

    var app = builder.Build();

    await app.UseDependenciesAsync();

    app.Run();

    Log.Information("Exiting...");

    return 0;

}
catch (Exception ex)
{
    Log.Fatal(ex, "An unhandled exception occurred during bootstrapping");

    if (Debugger.IsAttached)
    {
        throw;
    }

    return 1;
}
finally
{
    Log.CloseAndFlush();
}

public partial class Program;
