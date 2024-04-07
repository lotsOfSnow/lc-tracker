namespace LcTracker.Shared.Time;

public interface IAppClock
{
    public DateTimeOffset Now { get; }
}
