using LcTracker.Core.Features.Attempts;

namespace LcTracker.Core.Features.Problems.Queries.Export;

public class ExportedAttempt
{
    public Guid Id { get; set; }

    public DateOnly Date { get; set; }

    public string? Note { get; set; }

    public int? MinutesSpent { get; set; }

    public Difficulty Difficulty { get; set; }

    public bool HasUsedHelp { get; set; }

    public bool HasSolved { get; set; }

    public bool IsRecap { get; set; }

    public Guid ProblemId { get; set; }
}
