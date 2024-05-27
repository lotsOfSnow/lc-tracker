using LcTracker.Api.FunctionalTests.Bootstrap.TestBase;
using LcTracker.Api.FunctionalTests.Bootstrap.TestFixture;
using LcTracker.Core.Features.Problems;

namespace LcTracker.Api.FunctionalTests.Features.Problems;

public class ProblemTest(ApiTestFixture fixture) : BaseTest(fixture)
{
    protected Task<Problem> RequireProblem(Guid userId)
        => Context.Require.Object(
            Arrange
                .BuildWithUserId<Problem>(userId)
                .Without(x => x.IsLocked)
                .Create());
}
