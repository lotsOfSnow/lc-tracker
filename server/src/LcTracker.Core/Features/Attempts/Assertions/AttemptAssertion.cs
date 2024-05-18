namespace LcTracker.Core.Features.Attempts.Assertions;

public static class AttemptAssertion
{
    public static void DateIsInPast(TimeProvider timeProvider, DateOnly date)
    {
        var serverTime = timeProvider.GetUtcNow();

        var gracePeriod = TimeSpan.FromHours(24);
        var utcNowDate = DateOnly.FromDateTime(serverTime.Add(gracePeriod).UtcDateTime);
        if (date > utcNowDate)
        {
            throw new("Date can't be in future");
        }
    }
}
