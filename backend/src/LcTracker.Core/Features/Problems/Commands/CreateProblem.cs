using LcTracker.Core.Auth;
using LcTracker.Core.Features.LeetCode.Functions;
using LcTracker.Core.Features.Problems.Contracts;
using LcTracker.Core.Storage;
using LcTracker.Shared.Handlers;
using LcTracker.Shared.Results;

namespace LcTracker.Core.Features.Problems.Commands;

public record CreateProblemRequest(string Url, IEnumerable<ProblemMethodDto> Methods);

public record CreateProblemCommand(string Url, IEnumerable<ProblemMethodDto> Methods) : ICommand<Guid>;

public class CreateProblemCommandHandler(GetLeetCodeQuestion getLeetCodeQuestion, TimeProvider timeProvider, IGetCurrentUserId getCurrentUserId, AppDbContext appDbContext)
    : ICommandHandler<CreateProblemCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateProblemCommand command, CancellationToken ct = default)
    {
        var leetCodeQuestion = await getLeetCodeQuestion.ExecuteAsync(command.Url, ct);
        if (leetCodeQuestion is null)
        {
            return Errors.NotFound;
        }

        var now = timeProvider.GetUtcNow();
        var userId = getCurrentUserId.Execute();

        var problem = new Problem
        {
            Name = leetCodeQuestion.Title,
            Slug = leetCodeQuestion.TitleSlug,
            AppUserId = userId,
            AddedAt = now,
            Methods = ProblemMethodDto.Map(command.Methods),
        };

        await appDbContext.Problems.AddAsync(problem, ct);
        await appDbContext.SaveChangesAsync(ct);

        return problem.Id;
    }
}
