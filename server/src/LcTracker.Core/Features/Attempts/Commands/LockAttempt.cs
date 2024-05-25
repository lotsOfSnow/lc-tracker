using LcTracker.Core.Auth;
using LcTracker.Core.Features.Common;
using LcTracker.Core.Storage;

namespace LcTracker.Core.Features.Attempts.Commands;

public record LockAttempt(Guid Id, bool Value) : LockResourceCommand(Id, Value);

public class LockAttemptCommandHandler(AppDbContext dbContext, IGetCurrentUserId getCurrentUserId)
    : LockResourceCommandHandler<Attempt, LockAttempt>(dbContext, getCurrentUserId);
