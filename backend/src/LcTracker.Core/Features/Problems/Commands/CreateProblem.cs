using LcTracker.Core.Auth;
using LcTracker.Core.Features.Attempts;
using LcTracker.Core.Storage;
using LcTracker.Shared.Handlers;
using LcTracker.Shared.Time;

namespace LcTracker.Core.Features.Problems.Commands;

public record CreateProblemRequest(string Name, int Number, string Url);

public record CreateProblemCommand(string Name, int Number, string Url) : ICommand;

public class CreateProblemCommandHandler(IAppClock clock, IGetCurrentUserId getCurrentUserId, AppDbContext appDbContext)
    : ICommandHandler<CreateProblemCommand>
{
    public async Task HandleAsync(CreateProblemCommand command, CancellationToken ct = default)
    {
        var now = clock.Now;
        var userId = getCurrentUserId.Execute();

        var problem = new Problem
        {
            Name = command.Name,
            Number = command.Number,
            Url = command.Url,
            AppUserId = userId,
            AddedAt = now,
        };

        await appDbContext.Problems.AddAsync(problem, ct);

        await appDbContext.SaveChangesAsync(ct);
    }
}
