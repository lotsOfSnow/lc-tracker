using LcTracker.Core.Auth;
using LcTracker.Core.Features.LeetCode.Functions;
using LcTracker.Core.Features.Problems.Contracts;
using LcTracker.Core.Storage;
using LcTracker.Shared.Handlers;
using LcTracker.Shared.Results;

namespace LcTracker.Core.Features.Problems.Commands;

public record CreateProblemRequest(string Slug, IEnumerable<ProblemMethodDto> Methods);

public record CreateProblemCommand(string Slug, IEnumerable<ProblemMethodDto> Methods) : ICommand<Guid>;

public class CreateProblemCommandHandler(GetLeetCodeQuestion getLeetCodeQuestion, TimeProvider timeProvider, IGetCurrentUserId getCurrentUserId, AppDbContext appDbContext)
    : ICommandHandler<CreateProblemCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateProblemCommand command, CancellationToken ct = default)
    {
        var slug = command.Slug.Trim();
        LeetCodeQuestion? leetCodeQuestion = null;
        var forceManualTitle = slug.StartsWith('!') && slug.Length > 1;
        if (!forceManualTitle)
        {
            leetCodeQuestion = await getLeetCodeQuestion.ExecuteAsync(command.Slug, ct);
            if (leetCodeQuestion is null)
            {
                return Errors.NotFound;
            }
        }

        var now = timeProvider.GetUtcNow();
        var userId = getCurrentUserId.Execute();

        var problem = new Problem
        {
            Title = leetCodeQuestion?.Title ?? slug[1..],
            Slug = leetCodeQuestion?.TitleSlug,
            AppUserId = userId,
            AddedAt = now,
            Methods = ProblemMethodDto.Map(command.Methods),
        };

        await appDbContext.Problems.AddAsync(problem, ct);
        await appDbContext.SaveChangesAsync(ct);

        return problem.Id;
    }
}
