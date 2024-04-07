using LcTracker.Core.Features.Entries;

namespace LcTracker.Core.Features.Attempts;

public class Attempt
{
    public Guid Id { get; set; }

    public required Guid AppUserId { get; set; }

    public required Guid ProblemId { get; set; }

    public required DateTimeOffset Date { get; set; }

    public string? Description { get; set; }

    public int MinutesSpent { get; set; }

    public required Difficulty Difficulty { get; set; }

    public required bool HasUsedHelp { get; set; }

    public required bool HasSolved { get; set; }

    public required bool IsRecap { get; set; }
}
