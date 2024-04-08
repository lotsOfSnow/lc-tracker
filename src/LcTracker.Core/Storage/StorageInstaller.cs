using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace LcTracker.Core.Storage;

public static class StorageInstaller
{
    public static WebApplicationBuilder AddStorage(this WebApplicationBuilder builder)
    {
        var services = builder.Services;

        services
            .AddOptionsWithValidateOnStart<DatabaseOptions>()
            .BindConfiguration(DatabaseOptions.SectionName)
            .ValidateDataAnnotations();

        services.AddDbContext<AppDbContext>(
            (
                serviceProvider,
                optionsBuilder) =>
            {
                var databaseOptions = serviceProvider.GetRequiredService<IOptions<DatabaseOptions>>();
                optionsBuilder.UseSqlite(
                    databaseOptions.Value.ConnectionString);
            });

        services.AddScoped<IAppDbContext, AppDbContext>();

        return builder;
    }

    public static async Task UseStorageAsync(this WebApplication app)
    {
        await using var scope = app.Services.CreateAsyncScope();

        var context =
            scope.ServiceProvider
                .GetRequiredService<AppDbContext>();

        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();
    }
}
