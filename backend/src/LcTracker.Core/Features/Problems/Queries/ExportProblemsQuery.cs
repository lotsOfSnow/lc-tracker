using System.Text.Json;
using LcTracker.Core.Auth;
using LcTracker.Core.Storage;
using LcTracker.Shared.Handlers;
using Microsoft.EntityFrameworkCore;

namespace LcTracker.Core.Features.Problems.Queries;

public record ExportProblemsQuery : IQuery<ExportProblemsQueryResult>;

public record ExportProblemsQueryResult(string Json);

public class ExportProblemsQueryHandler(IAppDbContext dbContext, IGetCurrentUserId getCurrentUserId)
    : IQueryHandler<ExportProblemsQuery, ExportProblemsQueryResult>
{
    public async Task<ExportProblemsQueryResult> HandleAsync(ExportProblemsQuery command, CancellationToken ct)
    {
        var userId = getCurrentUserId.Execute();

        var json =  await GetJson(userId);

        return new(json);
    }

    private async Task<string> GetJson(Guid userId)
    {
        var problems = await dbContext.Problems
            .Where(x => x.AppUserId == userId)
            .Include(x => x.Attempts!.Where(a => a.AppUserId == userId))
            .ToListAsync();

        return JsonSerializer.Serialize(problems);
    }
}
