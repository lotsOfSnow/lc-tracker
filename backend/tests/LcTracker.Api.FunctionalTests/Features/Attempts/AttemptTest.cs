using LcTracker.Core.Features.Attempts;
using LcTracker.Core.Features.Problems;

namespace LcTracker.Api.FunctionalTests.Features.Attempts;

public abstract class AttemptTest(ApiTestFixture fixture) : BaseTest(fixture)
{
    protected Task<Problem> RequireProblem(Guid userId)
        => Context.Require.Object(
            Arrange
                .BuildWithUserId<Problem>(userId)
                .Create());

    protected Task<Attempt> RequireAttempt(Problem problem, Guid userId, DateTimeOffset serverTime)
        => Context.Require.Object(
            new Attempt(serverTime, DateOnly.FromDateTime(serverTime.AddDays(-1).Date))
            {
                AppUserId = userId,
                ProblemId = problem.Id,
                Difficulty = Arrange.Create<Difficulty>(),
                HasSolved = Arrange.Create<bool>(),
                IsRecap = Arrange.Create<bool>(),
                HasUsedHelp = Arrange.Create<bool>(),
            });
}
