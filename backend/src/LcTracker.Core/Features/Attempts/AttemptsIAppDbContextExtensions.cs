using LcTracker.Core.Auth;
using LcTracker.Core.Storage;

namespace LcTracker.Core.Features.Attempts;

public static class AttemptsAppDbContextExtensions
{
    public static IQueryable<Attempt> UserAttempts(this IAppDbContext dbContext, IGetCurrentUserId getCurrentUserId)
        => dbContext.Attempts.Where(x => x.AppUserId == getCurrentUserId.Execute());
}
