namespace LcTracker.Shared.Entities;

public static class EntitiesQueryableExtensions
{
    public static IQueryable<T> OwnedBy<T>(this IQueryable<T> owned, Guid appUserId)
        where T : IOwned
        => owned.Where(x => x.AppUserId == appUserId);
}
