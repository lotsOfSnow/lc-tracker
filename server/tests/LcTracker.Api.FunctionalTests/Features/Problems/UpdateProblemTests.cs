using LcTracker.Api.FunctionalTests.Bootstrap.TestBase;
using LcTracker.Api.FunctionalTests.Bootstrap.TestFixture;
using LcTracker.Core.Features.Problems;
using LcTracker.Core.Features.Problems.Commands;

namespace LcTracker.Api.FunctionalTests.Features.Problems;

[Collection(FeaturesCollection.Name)]
public class UpdateProblemTests(ApiTestFixture fixture) : ProblemTest(fixture)
{
    [Fact]
    public async Task Updates_existing()
    {
        var userId = await Client.RunAsNewUserAsync();
        var problem = await RequireProblem(userId);
        var request = Arrange.Create<UpdateProblemRequest>();

        var result = await Client.PutAsync<EmptyResponse>($"api/problems/{problem.Id}", request);

        result.Response.Should().BeSuccessful();
        var updated = await Context.Database.GetAsync<Problem>(problem.Id);
        updated.Should().BeEquivalentTo(request);
    }
}
