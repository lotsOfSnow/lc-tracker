using LcTracker.Core.Auth;
using LcTracker.Core.Features.Common;
using LcTracker.Core.Storage;

namespace LcTracker.Core.Features.Problems.Commands;

public record LockProblem(Guid Id, bool Value) : LockResourceCommand(Id, Value);

public class LockProblemCommandHandler(AppDbContext dbContext, IGetCurrentUserId getCurrentUserId)
    : LockResourceCommandHandler<Problem, LockProblem>(dbContext, getCurrentUserId);
