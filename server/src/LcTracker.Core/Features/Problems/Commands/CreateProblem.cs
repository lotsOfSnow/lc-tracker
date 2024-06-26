using LcTracker.Core.Auth;
using LcTracker.Core.Features.LeetCode.Functions;
using LcTracker.Core.Features.Problems.Contracts;
using LcTracker.Core.Storage;
using LcTracker.Shared.Handlers;
using LcTracker.Shared.Results;

namespace LcTracker.Core.Features.Problems.Commands;

public record CreateProblemRequest(string Slug, string? Note, IEnumerable<ProblemMethodDto>? Methods);

public record CreateProblemResponse(Guid Id);

public record CreateProblemCommand(string Slug, string? Note, IEnumerable<ProblemMethodDto>? Methods) : ICommand<Guid>;

public class CreateProblemCommandHandler(GetLeetCodeQuestion getLeetCodeQuestion, TimeProvider timeProvider, IGetCurrentUserId getCurrentUserId, AppDbContext appDbContext)
    : ICommandHandler<CreateProblemCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateProblemCommand command, CancellationToken ct = default)
    {
        var slug = command.Slug.Trim();
        LeetCodeQuestion? leetCodeQuestion = null;
        const string forcePrefix = "!";
        var forceManualTitle = slug.StartsWith(forcePrefix) && slug.Length > forcePrefix.Length;
        if (!forceManualTitle)
        {
            leetCodeQuestion = await getLeetCodeQuestion.ExecuteAsync(command.Slug, ct);
            if (leetCodeQuestion is null)
            {
                return Errors.NotFound.Because("Couldn't find question using this slug").Create();
            }
        }

        var now = timeProvider.GetUtcNow().UtcDateTime;
        var userId = getCurrentUserId.Execute();

        var problem = new Problem
        {
            Title = leetCodeQuestion?.Title ?? slug[forcePrefix.Length..],
            Slug = leetCodeQuestion?.TitleSlug,
            Note = command.Note?.Trim(),
            AppUserId = userId,
            AddedAt = now,
            Methods = ProblemMethodDto.Map(command.Methods),
        };

        await appDbContext.Problems.AddAsync(problem, ct);
        await appDbContext.SaveChangesAsync(ct);

        return problem.Id;
    }
}
