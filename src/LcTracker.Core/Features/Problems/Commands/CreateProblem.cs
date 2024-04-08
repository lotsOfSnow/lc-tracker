using LcTracker.Core.Auth;
using LcTracker.Core.Features.Attempts;
using LcTracker.Core.Handlers;
using LcTracker.Core.Storage;
using LcTracker.Shared.Time;

namespace LcTracker.Core.Features.Problems.Commands;

public static class CreateProblem
{
    public record Command(string Name, int Number, string Url) : ICommand;

    public class Handler(IAppClock clock, IGetCurrentUserId getCurrentUserId, AppDbContext appDbContext) : ICommandHandler<Command>
    {
        public async Task HandleAsync(Command command, CancellationToken ct = default)
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
}
