using LcTracker.Core.Features.Attempts;
using LcTracker.Core.Features.Problems;
using Riok.Mapperly.Abstractions;

namespace LcTracker.Core.Features.CurrentUser.Queries.Export;

[Mapper]
public static partial class ExportCurrentUserDataMapper
{
    public static partial ExportedProblem ToExported(this Problem src);

    public static partial ExportedAttempt ToExported(this Attempt src);
}
