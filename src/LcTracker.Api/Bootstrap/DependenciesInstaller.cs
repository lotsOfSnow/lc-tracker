using LcTracker.Core.Auth;
using LcTracker.Core.Features.Attempts.Commands;
using LcTracker.Core.Storage;
using LcTracker.Shared.Handlers;
using LcTracker.Shared.Time;
using LcTracker.Shared.Web.Cors;

namespace LcTracker.Api.Bootstrap;

public static class DependenciesInstaller
{
    public static void AddDependencies(this WebApplicationBuilder builder)
    {
        builder.AddShared();

        builder.AddCore();

        builder.AddStorage();

        builder.AddApi();
    }

    public static async Task UseDependenciesAsync(this WebApplication app)
    {
        await app.UseStorageAsync();

        app.UseAppCors();

        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseHttpsRedirection();

        app.UseStaticFiles();

        app.MapControllers();
    }

    private static void AddShared(this WebApplicationBuilder builder)
    {
        builder.Services.AddAppClock();
        builder.Services.AddTransient<IDispatcher, Dispatcher>();
    }

    private static void AddCore(this WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<IGetCurrentUserId, GetCurrentUserId>();
        builder.Services.AddHandlers(typeof(CreateAttemptCommandHandler).Assembly);
    }

    private static void AddApi(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddControllers();

        builder.AddAppCors();
    }
}
