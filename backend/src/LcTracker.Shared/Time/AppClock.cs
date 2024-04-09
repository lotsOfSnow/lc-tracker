namespace LcTracker.Shared.Time;

public class AppClock : IAppClock
{
    public DateTimeOffset Now => DateTimeOffset.UtcNow;
}
