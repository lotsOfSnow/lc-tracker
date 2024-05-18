using LcTracker.Core.Auth;
using LcTracker.Core.Storage;
using LcTracker.Shared.Entities;
using LcTracker.Shared.Handlers;
using LcTracker.Shared.Results;
using Microsoft.EntityFrameworkCore;

namespace LcTracker.Core.Features.CurrentUser.Queries.Export;

public record ExportCurrentUserDataQuery : IQuery<ExportCurrentUserDataQueryResult>;

public record ExportCurrentUserDataQueryResult(ICollection<ExportedProblem> Problems, ICollection<ExportedAttempt> Attempts);

public class ExportCurrentUserDataQueryHandler(IAppDbContext dbContext, IGetCurrentUserId getCurrentUserId)
    : IQueryHandler<ExportCurrentUserDataQuery, ExportCurrentUserDataQueryResult>
{
    public async Task<Result<ExportCurrentUserDataQueryResult>> Handle(ExportCurrentUserDataQuery command, CancellationToken ct)
    {
        var userId = getCurrentUserId.Execute();

        var problems = await dbContext.Problems
            .OwnedBy(userId)
            .AsNoTrackingWithIdentityResolution()
            .Select(
                x => x.ToExported())
            .ToListAsync(ct);

        var attempts = await dbContext.Attempts
            .OwnedBy(userId)
            .AsNoTrackingWithIdentityResolution()
            .Select(
                x => x.ToExported())
            .ToListAsync(ct);

        return new ExportCurrentUserDataQueryResult(problems, attempts);
    }
}
