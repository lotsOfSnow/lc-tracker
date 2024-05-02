using LcTracker.Shared.Entities;

namespace LcTracker.Core.Features.Problems;

public class Problem : IOwned
{
    public Guid Id { get; set; }

    public required Guid AppUserId { get; set; }

    public required int Number { get; set; }

    public required string Name { get; set; }

    public required string Url { get; set; }

    public required DateTimeOffset AddedAt { get; set; }

    // TODO: Hints, solutions, description.
}
