using LcTracker.Core.Auth;
using LcTracker.Core.Storage;
using LcTracker.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace LcTracker.Core.Common;

public class LockService(AppDbContext dbContext, IGetCurrentUserId getCurrentUserId)
{
    public Task LockAsync<T>(Guid id, CancellationToken ct) where T : Entity, ILockable
        => Execute<T>(id, true, ct);

    public Task UnlockAsync<T>(Guid id, CancellationToken ct) where T : Entity, ILockable
        => Execute<T>(id, false, ct);

    private async Task Execute<T>(Guid id, bool valueToSet, CancellationToken ct) where T : Entity, ILockable
    {
        var entity = await GetEntity<T>(id, ct);
        if (entity is null)
        {
            return;
        }

        entity.IsLocked = valueToSet;

        await dbContext.SaveChangesAsync(ct);
    }

    private async Task<T?> GetEntity<T>(Guid id, CancellationToken ct) where T : Entity
    {
        var entity = await dbContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id, ct);

        return entity is null
               || entity is IOwned owned && owned.AppUserId != getCurrentUserId.Execute()
            ? null
            : entity;
    }
}

public interface ILockable
{
    public bool IsLocked { get; set; }
}
