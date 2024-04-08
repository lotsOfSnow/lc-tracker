using LcTracker.Core.Auth;
using LcTracker.Core.Features.Attempts.Commands;
using LcTracker.Core.Storage;
using LcTracker.Shared.Handlers;
using LcTracker.Shared.Time;

namespace LcTracker.Api.Bootstrap;

public static class DependenciesInstaller
{
    public static void AddDependencies(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddAppClock();
        builder.Services.AddTransient<IGetCurrentUserId, GetCurrentUserId>();
        builder.Services.AddTransient<IDispatcher, Dispatcher>();
        builder.Services.AddHandlers(typeof(CreateAttemptCommandHandler).Assembly);

        builder.Services.AddControllers();

        builder.AddStorage();
    }

    public static async Task UseDependenciesAsync(this WebApplication app)
    {
        await app.UseStorageAsync();

        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseHttpsRedirection();

        app.UseStaticFiles();

        app.MapControllers();
    }
}
