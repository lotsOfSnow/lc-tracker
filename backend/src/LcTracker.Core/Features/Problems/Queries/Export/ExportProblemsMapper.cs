using LcTracker.Core.Features.Attempts;
using Riok.Mapperly.Abstractions;

namespace LcTracker.Core.Features.Problems.Queries.Export;

[Mapper]
public static partial class ExportProblemsMapper
{
    public static partial ExportedProblem ToExported(this Problem src);

    public static partial ExportedAttempt ToExported(this Attempt src);
}
