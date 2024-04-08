using LcTracker.Core.Auth;
using LcTracker.Core.Features.Attempts;
using LcTracker.Core.Storage;

namespace LcTracker.Core.Features.Problems;

public static class ProblemsAppDbContextExtensions
{
    public static IQueryable<Problem> UserProblems(this IAppDbContext dbContext, IGetCurrentUserId getCurrentUserId)
        => dbContext.Problems.Where(x => x.AppUserId == getCurrentUserId.Execute());
}
