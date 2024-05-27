using LcTracker.Api.FunctionalTests.Bootstrap.TestBase;
using LcTracker.Api.FunctionalTests.Bootstrap.TestFixture;
using LcTracker.Core.Features.Problems;

namespace LcTracker.Api.FunctionalTests.Features.Problems;

[Collection(FeaturesCollection.Name)]
public class LockProblemTests(ApiTestFixture fixture) : ProblemTest(fixture)
{
    [Fact]
    public async Task Locks()
    {
        var userId = await Client.RunAsNewUserAsync();
        var problem = await RequireProblem(userId);

        var result = await Client.PutAsync<EmptyResponse>($"api/problems/{problem.Id}/lock");

        result.Response.Should().BeSuccessful();
        var locked = await Context.Database.GetAsync<Problem>(problem.Id);
        locked.IsLocked.Should().BeTrue();
    }

    [Fact]
    public async Task Unlocks()
    {
        var userId = await Client.RunAsNewUserAsync();
        var problem = await RequireProblem(userId);
        await Client.PutAsync<EmptyResponse>($"api/problems/{problem.Id}/lock");

        var result = await Client.DeleteAsync<EmptyResponse>($"api/problems/{problem.Id}/lock");

        result.Response.Should().BeSuccessful();
        var locked = await Context.Database.GetAsync<Problem>(problem.Id);
        locked.IsLocked.Should().BeFalse();
    }
}
