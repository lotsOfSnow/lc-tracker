using LcTracker.Shared.Entities;

namespace LcTracker.Core.Features.Problems;

public class Problem : Entity, IOwned
{
    public required Guid AppUserId { get; set; }

    public required string Title { get; set; }

    public required string? Slug { get; set; }

    public required string? Note { get; set; }

    public required DateTime AddedAt { get; set; }

    public HashSet<ProblemMethod> Methods { get; set; } = null!;
}
