using LcTracker.Core.Auth;
using LcTracker.Core.Features.Problems.Contracts;
using LcTracker.Core.Storage;
using LcTracker.Shared.Handlers;
using LcTracker.Shared.Results;

namespace LcTracker.Core.Features.Problems.Commands;

public record CreateProblemRequest(string Name, int Number, string Url, IEnumerable<ProblemMethodDto> Methods);

public record CreateProblemCommand(string Name, int Number, string Url, IEnumerable<ProblemMethodDto> Methods) : ICommand<Guid>;

public class CreateProblemCommandHandler(TimeProvider timeProvider, IGetCurrentUserId getCurrentUserId, AppDbContext appDbContext)
    : ICommandHandler<CreateProblemCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateProblemCommand command, CancellationToken ct = default)
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
            Methods = ProblemMethodDto.Map(command.Methods),
        };

        await appDbContext.Problems.AddAsync(problem, ct);

        await appDbContext.SaveChangesAsync(ct);

        return problem.Id;
    }
}
