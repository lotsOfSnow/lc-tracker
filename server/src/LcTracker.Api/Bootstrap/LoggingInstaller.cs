using Serilog;
using Serilog.Debugging;

namespace LcTracker.Api.Bootstrap;

public static class LoggingInstaller
{
    public static void AddSerilogLogging(
        this WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog((
                context,
                services,
                configuration) =>
            configuration
                .ReadFrom.Configuration(context.Configuration)
                .ReadFrom.Services(services));

        if (builder.Environment.IsDevelopment())
        {
            SelfLog.Enable(Console.Error);
        }
    }

    public static void UseSerilogConfiguration(
        this IApplicationBuilder app)
    {
        app.UseSerilogRequestLogging(options =>
        {
            options.MessageTemplate = "Handled {RequestPath}";

            options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
            {
                diagnosticContext.Set("RequestHost", httpContext.Request.Host.Value);
                diagnosticContext.Set("RequestScheme", httpContext.Request.Scheme);
            };
        });
    }
}
