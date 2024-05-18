using LcTracker.Shared.Entities;

namespace LcTracker.Core.Features.Attempts;

public class Attempt : Entity, IOwned
{
    private int? _minutesSpent;
    private string? _note;

    public required Guid AppUserId { get; set; }

    public required Guid ProblemId { get; set; }

    public required DateOnly Date { get; set; }

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
}
