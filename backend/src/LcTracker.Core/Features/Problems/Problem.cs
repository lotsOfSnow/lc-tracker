using LcTracker.Core.Features.Attempts;

namespace LcTracker.Core.Features.Problems;

public class Problem
{
    public Guid Id { get; set; }

    public required Guid AppUserId { get; set; }

    public required int Number { get; set; }

    public required string Name { get; set; }

    public required string Url { get; set; }

    public required DateTimeOffset AddedAt { get; set; }

    public ICollection<Attempt>? Attempts { get; set; }

    // TODO: Hints, solutions, description.
}
