using LcTracker.Core.Auth;
using LcTracker.Core.Storage;
using LcTracker.Shared.Handlers;

namespace LcTracker.Core.Features.Attempts.Commands;

public record CreateAttemptRequest(
    Guid ProblemId,
    int? MinutesSpent,
    DateOnly Date,
    string Note,
    bool HasUsedHelp,
    bool HasSolved,
    bool IsRecap,
    Difficulty Difficulty);

public record CreateAttemptCommand(
    Guid ProblemId,
    int? MinutesSpent,
    DateOnly Date,
    string Note,
    bool HasUsedHelp,
    bool HasSolved,
    bool IsRecap,
    Difficulty Difficulty) : ICommand;

public class CreateAttemptCommandHandler(TimeProvider timeProvider,
    IAppDbContext dbContext,
    IGetCurrentUserId getCurrentUserId) : ICommandHandler<CreateAttemptCommand>
{
    public async Task Handle(CreateAttemptCommand command, CancellationToken ct)
    {
        var userId = getCurrentUserId.Execute();

        var attempt = new Attempt(timeProvider.GetUtcNow(), command.Date)
        {
            ProblemId = command.ProblemId,
            Difficulty = command.Difficulty,
            HasSolved = command.HasSolved,
            IsRecap = command.IsRecap,
            MinutesSpent = command.MinutesSpent,
            HasUsedHelp = command.HasUsedHelp,
            Note = command.Note,
            AppUserId = userId,
        };

        await dbContext.Attempts.AddAsync(attempt, ct);

        await dbContext.SaveChangesAsync(ct);
    }
}
