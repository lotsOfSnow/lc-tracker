using LcTracker.Shared.Entities;

namespace LcTracker.Core.Features.Problems;

public class Problem : IOwned
{
    public Guid Id { get; set; }

    public required Guid AppUserId { get; set; }

    public required string Title { get; set; }

    public required string? Slug { get; set; }

    public required string? Note { get; set; }

    public required DateTimeOffset AddedAt { get; set; }

    public HashSet<ProblemMethod> Methods { get; set; } = null!;
}
