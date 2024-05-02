namespace LcTracker.Core.Features.CurrentUser.Queries.Export;

public class ExportedProblem
{
    public Guid Id { get; set; }

    public required int Number { get; set; }

    public required string Name { get; set; }

    public required string Url { get; set; }

    public required DateTimeOffset AddedAt { get; set; }
}
