using Microsoft.OpenApi.Any;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace LcTracker.Api.Bootstrap;

public static class OpenApiInstaller
{
    public static void AddOpenApi(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddSwaggerGen(c =>
        {
            c.CustomOperationIds(apiDesc => apiDesc
                .TryGetMethodInfo(out var methodInfo) ?
                $"{methodInfo.DeclaringType!.Name.Replace("Controller", "")}.{methodInfo.Name}"
                : null);

            c.MapType<DateOnly>(() => new()
            {
                Type = "string",
                Format = "date",
                Example = new OpenApiString("2024-04-20"),
            });

        });
    }

    public static void UseOpenApi(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
}
