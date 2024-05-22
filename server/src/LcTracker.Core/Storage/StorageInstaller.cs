using LcTracker.Shared.Options;
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

        services.AddValidatedOptions<DatabaseOptions>(DatabaseOptions.SectionName);

        services.AddDbContext<AppDbContext>(
            (
                serviceProvider,
                optionsBuilder) =>
            {
                var databaseOptions = serviceProvider.GetRequiredService<IOptions<DatabaseOptions>>();
                optionsBuilder.UseNpgsql(
                    databaseOptions.Value.ConnectionString);
            });

        services.AddScoped<IAppDbContext, AppDbContext>();

        return builder;
    }
}
