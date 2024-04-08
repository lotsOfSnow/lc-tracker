namespace LcTracker.Core.Features.Attempts;

public class Attempt
{
    public Guid Id { get; set; }

    public required Guid AppUserId { get; set; }

    public required Guid ProblemId { get; set; }

    public required DateTimeOffset Date { get; set; }

    public string? Note { get; set; }

    public int MinutesSpent { get; set; }

    public required Difficulty PerceivedDifficulty { get; set; }

    public required bool HasUsedHelp { get; set; }

    public required bool HasSolved { get; set; }

    public required bool IsRecap { get; set; }
}
