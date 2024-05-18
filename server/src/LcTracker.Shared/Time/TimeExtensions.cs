using Microsoft.Extensions.DependencyInjection;

namespace LcTracker.Shared.Time;

public static class TimeExtensions
{
    public static IServiceCollection AddTimeProvider(this IServiceCollection services)
        => services.AddSingleton(TimeProvider.System);
}
