using LcTracker.Core.Auth;
using LcTracker.Core.Storage;
using LcTracker.Shared.Handlers;

namespace LcTracker.Core.Features.Problems.Commands;

public record CreateProblemMethod(string Name, string Contents);

public record CreateProblemRequest(string Name, int Number, string Url, IEnumerable<CreateProblemMethod> Methods);

public record CreateProblemCommand(string Name, int Number, string Url, IEnumerable<CreateProblemMethod> Methods) : ICommand;

public class CreateProblemCommandHandler(TimeProvider timeProvider, IGetCurrentUserId getCurrentUserId, AppDbContext appDbContext)
    : ICommandHandler<CreateProblemCommand>
{
    public async Task Handle(CreateProblemCommand command, CancellationToken ct = default)
    {
        var now = timeProvider.GetUtcNow();
        var userId = getCurrentUserId.Execute();

        var problem = new Problem
        {
            Name = command.Name,
            Number = command.Number,
            Url = command.Url,
            AppUserId = userId,
            AddedAt = now,
            Methods = command.Methods.Select(x => new ProblemMethod(x.Name, x.Contents)).ToHashSet(),
        };

        await appDbContext.Problems.AddAsync(problem, ct);

        await appDbContext.SaveChangesAsync(ct);
    }
}
