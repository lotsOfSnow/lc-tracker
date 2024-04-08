using LcTracker.Core.Auth;
using LcTracker.Core.Storage;
using LcTracker.Shared.Handlers;
using Microsoft.EntityFrameworkCore;

namespace LcTracker.Core.Features.Attempts.Commands;

public record DeleteAttemptCommand(Guid Id) : ICommand;

public class DeleteAttemptCommandHandler(IGetCurrentUserId getCurrentUserId, IAppDbContext dbContext)
    : ICommandHandler<DeleteAttemptCommand>
{
    public async Task HandleAsync(DeleteAttemptCommand command, CancellationToken ct)
    {
        var userId = getCurrentUserId.Execute();

        var attempt = await dbContext.Attempts.FirstOrDefaultAsync(x => x.Id == command.Id && x.AppUserId == userId, ct);

        if (attempt is null)
        {
            return;
        }

        dbContext.Attempts.Remove(attempt);

        await dbContext.SaveChangesAsync(ct);
    }
}
