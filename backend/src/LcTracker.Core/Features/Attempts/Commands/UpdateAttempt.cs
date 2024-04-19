using LcTracker.Core.Auth;
using LcTracker.Core.Storage;
using LcTracker.Shared.Handlers;
using Microsoft.EntityFrameworkCore;

namespace LcTracker.Core.Features.Attempts.Commands;

public record UpdateAttemptRequest(
    Guid ProblemId,
    int? MinutesSpent,
    DateOnly Date,
    string Note,
    bool HasUsedHelp,
    bool HasSolved,
    bool IsRecap,
    Difficulty Difficulty);

public record UpdateAttemptCommand(
    Guid Id,
    Guid ProblemId,
    int? MinutesSpent,
    DateOnly Date,
    string Note,
    bool HasUsedHelp,
    bool HasSolved,
    bool IsRecap,
    Difficulty Difficulty) : ICommand;

public class UpdateAttemptCommandHandler(TimeProvider timeProvider, IAppDbContext dbContext, IGetCurrentUserId getCurrentUserId) : ICommandHandler<UpdateAttemptCommand>
{
    public async Task HandleAsync(UpdateAttemptCommand command, CancellationToken ct)
    {
        var attempt = await dbContext
            .UserAttempts(getCurrentUserId)
            .FirstOrDefaultAsync(x => x.Id == command.Id, ct);

        if (attempt is null)
        {
            throw new("No attempt found");
        }

        attempt.ProblemId = command.ProblemId;
        attempt.MinutesSpent = command.MinutesSpent;
        attempt.UpdateDate(timeProvider.GetUtcNow(), command.Date);
        attempt.Note = command.Note;
        attempt.HasUsedHelp = command.HasUsedHelp;
        attempt.HasSolved = command.HasSolved;
        attempt.IsRecap = command.IsRecap;
        attempt.Difficulty = command.Difficulty;

        await dbContext.SaveChangesAsync(ct);
    }
}
