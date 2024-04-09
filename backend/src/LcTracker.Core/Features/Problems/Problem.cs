namespace LcTracker.Core.Features.Attempts;

public class Problem
{
    public Guid Id { get; set; }

    public required Guid AppUserId { get; set; }

    public required int Number { get; set; }

    public required string Name { get; set; }

    public required string Url { get; set; }

    public required DateTimeOffset AddedAt { get; set; }

    // TODO: Hints, solutions, description.
}
