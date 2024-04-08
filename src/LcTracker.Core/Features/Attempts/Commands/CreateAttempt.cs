using LcTracker.Core.Auth;
using LcTracker.Core.Handlers;
using LcTracker.Core.Storage;
using LcTracker.Shared.Time;

namespace LcTracker.Core.Features.Attempts.Commands;

public record CreateAttemptRequest(Guid ProblemId, int MinutesSpent);

public record CreateAttemptCommand(Guid ProblemId, int MinutesSpent) : ICommand;

public class CreateAttemptCommandHandler(IAppClock appClock, IAppDbContext dbContext, IGetCurrentUserId getCurrentUserId) : ICommandHandler<CreateAttemptCommand>
{
    /// <inheritdoc />
    public async Task HandleAsync(CreateAttemptCommand command, CancellationToken ct)
    {
        var now = appClock.Now;
        var userId = getCurrentUserId.Execute();

        var attempt = new Attempt()
        {
            ProblemId = command.ProblemId,
            Date = now,
            PerceivedDifficulty = Difficulty.Easy,
            HasSolved = true,
            IsRecap = false,
            MinutesSpent = command.MinutesSpent,
            HasUsedHelp = false,
            Note = "",
            AppUserId = userId,
        };

        await dbContext.Attempts.AddAsync(attempt, ct);

        await dbContext.SaveChangesAsync(ct);
    }
}
