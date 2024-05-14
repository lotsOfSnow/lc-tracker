using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Time.Testing;

namespace LcTracker.Api.FunctionalTests.Bootstrap.Context;

public class TestContextTime(
    IServiceProvider services)
{
    public DateTimeOffset SetUtcNow(
        DateTime now)
    {
        var clock = services.GetRequiredService<TimeProvider>();

        if (clock is not FakeTimeProvider testClock)
        {
            throw new InvalidOperationException(
                $"{typeof(TimeProvider)} clock wasn't replaced with {typeof(FakeTimeProvider)} in test app setup");
        }

        var nowAsUtc = now.AsUtc();
        testClock.SetUtcNow(nowAsUtc);

        return nowAsUtc;
    }
}
