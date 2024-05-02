using LcTracker.Core.Auth;
using LcTracker.Core.Storage;
using LcTracker.Shared.Entities;
using LcTracker.Shared.Handlers;
using Microsoft.EntityFrameworkCore;

namespace LcTracker.Core.Features.Problems.Queries.Export;

public record ExportProblemsQuery : IQuery<ExportProblemsQueryResult>;

public record ExportProblemsQueryResult(ICollection<ExportedProblem> Problems, ICollection<ExportedAttempt> Attempts);

public class ExportProblemsQueryHandler(IAppDbContext dbContext, IGetCurrentUserId getCurrentUserId)
    : IQueryHandler<ExportProblemsQuery, ExportProblemsQueryResult>
{
    public async Task<ExportProblemsQueryResult> Handle(ExportProblemsQuery command, CancellationToken ct)
    {
        var userId = getCurrentUserId.Execute();

        var problems = await dbContext.Problems
            .OwnedBy(userId)
            .AsNoTrackingWithIdentityResolution()
            .Select(
                x => x.ToExported())
            .ToListAsync();

        var attempts = await dbContext.Attempts
            .OwnedBy(userId)
            .AsNoTrackingWithIdentityResolution()
            .Select(
                x => x.ToExported())
            .ToListAsync();

        return new(problems, attempts);
    }
}
