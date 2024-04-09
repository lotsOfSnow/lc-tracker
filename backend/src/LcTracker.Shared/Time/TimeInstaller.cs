using Microsoft.Extensions.DependencyInjection;

namespace LcTracker.Shared.Time;

public static class TimeInstaller
{
    public static IServiceCollection AddAppClock(this IServiceCollection services)
        => services.AddTransient<IAppClock, AppClock>();
}
