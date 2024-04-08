using LcTracker.Core.Auth;
using LcTracker.Core.Storage;
using LcTracker.Shared.Handlers;
using Microsoft.EntityFrameworkCore;

namespace LcTracker.Core.Features.Attempts.Commands;

public record UpdateAttemptRequest(Guid ProblemId, int MinutesSpent);

public record UpdateAttemptCommand(Guid Id, Guid ProblemId, int MinutesSpent) : ICommand;

public class UpdateAttemptCommandHandler(IAppDbContext dbContext, IGetCurrentUserId getCurrentUserId) : ICommandHandler<UpdateAttemptCommand>
{
    /// <inheritdoc />
    public async Task HandleAsync(UpdateAttemptCommand command, CancellationToken ct)
    {
        var attempt = await dbContext
            .UserAttempts(getCurrentUserId)
            .FirstOrDefaultAsync(x => x.Id == command.Id, ct);

        if (attempt is null)
        {
            throw new Exception("No attempt found");
        }

        attempt.ProblemId = command.ProblemId;
        attempt.MinutesSpent = command.MinutesSpent;

        await dbContext.SaveChangesAsync(ct);
    }
}
