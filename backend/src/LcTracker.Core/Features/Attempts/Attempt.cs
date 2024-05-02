using JetBrains.Annotations;
using LcTracker.Shared.Entities;

namespace LcTracker.Core.Features.Attempts;

public class Attempt : IOwned
{
    private int? _minutesSpent;
    private string? _note;

    public Attempt(DateTimeOffset now, DateOnly date)
    {
        UpdateDate(now, date);
    }

    public Guid Id { get; set; }

    public required Guid AppUserId { get; set; }

    public required Guid ProblemId { get; set; }

    public DateOnly Date { get; private set; }

    public string? Note
    {
        get => _note;
        set => _note = value?.Trim();
    }

    public int? MinutesSpent
    {
        get => _minutesSpent;
        set
        {
            if (value <= 0)
            {
                // TODO
                throw new();
            }

            _minutesSpent = value;
        }
    }

    public required Difficulty Difficulty { get; set; }

    public required bool HasUsedHelp { get; set; }

    public required bool HasSolved { get; set; }

    public required bool IsRecap { get; set; }

    public void UpdateDate(DateTimeOffset serverTime, DateOnly date)
    {
        var gracePeriod = TimeSpan.FromHours(24);
        var utcNowDate = DateOnly.FromDateTime(serverTime.Add(gracePeriod).UtcDateTime);
        if (date > utcNowDate)
        {
            throw new("Date can't be in future");
        }

        Date = date;
    }

    [UsedImplicitly]
    private Attempt(){}
}
