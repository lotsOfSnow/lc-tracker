namespace LcTracker.Core.Features.CurrentUser.Queries.Export;

public class ExportedProblem
{
    public Guid Id { get; set; }

    public required string Title { get; set; }

    public required string? Slug { get; set; }

    public required DateTimeOffset AddedAt { get; set; }
}
