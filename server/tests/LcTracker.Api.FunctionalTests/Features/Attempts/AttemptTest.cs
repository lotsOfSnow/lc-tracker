using LcTracker.Api.FunctionalTests.Bootstrap.TestBase;
using LcTracker.Api.FunctionalTests.Bootstrap.TestFixture;
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

    protected Task<Attempt> RequireAttempt(Problem problem, Guid userId)
        => Context.Require.Object(Arrange
            .BuildWithUserId<Attempt>(userId)
            .With(x => x.ProblemId, problem.Id)
            .Without(x => x.IsLocked)
            .Create());

    protected async Task<Attempt> RequireAttempt(Guid userId)
    {
        var problem = await RequireProblem(userId);

        return await RequireAttempt(problem, userId);
    }
}
