using LcTracker.Core.Auth;
using LcTracker.Core.Storage;
using LcTracker.Shared.Entities;
using LcTracker.Shared.Handlers;
using LcTracker.Shared.Results;
using Microsoft.EntityFrameworkCore;

namespace LcTracker.Core.Features.Common;

public abstract record LockResourceCommand(Guid Id, bool Value) : ICommand;

public abstract class LockResourceCommandHandler<T, TCommand>(AppDbContext dbContext, IGetCurrentUserId getCurrentUserId)
    : ICommandHandler<TCommand>
    where T : Entity, ILockable
    where TCommand : LockResourceCommand
{
    public async Task<Result> Handle(TCommand request, CancellationToken cancellationToken)
    {
        var entity = await GetEntity(request.Id, cancellationToken);
        if (entity is null)
        {
            return Errors.NotFound.Create();
        }

        if (!entity.IsLocked && !request.Value)
        {
            return Errors.NotFound.Because("Lock not found").Create();
        }

        entity.IsLocked = request.Value;

        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.Ok;
    }

    private async Task<T?> GetEntity(Guid id, CancellationToken ct)
    {
        var entity = await dbContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id, ct);

        return entity is null
               || entity is IOwned owned && owned.AppUserId != getCurrentUserId.Execute()
            ? null
            : entity;
    }
}
