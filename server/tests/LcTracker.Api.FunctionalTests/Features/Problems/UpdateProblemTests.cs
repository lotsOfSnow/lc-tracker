using LcTracker.Api.FunctionalTests.Bootstrap.TestBase;
using LcTracker.Api.FunctionalTests.Bootstrap.TestFixture;
using LcTracker.Core.Features.Problems;
using LcTracker.Core.Features.Problems.Commands;
using Microsoft.AspNetCore.Mvc;

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

    [Fact]
    public async Task Disallows_updating_locked()
    {
        var userId = await Client.RunAsNewUserAsync();
        var original = await Context.Require.Object(
            Arrange
                .BuildWithUserId<Problem>(userId)
                .With(x => x.IsLocked, true)
                .Create());
        var request = Arrange.Create<UpdateProblemRequest>();

        var result = await Client.PutAsync<ProblemDetails>($"api/problems/{original.Id}", request);

        result.Response.Should().HaveError();
        result.Data.Should().BeEquivalentTo(new
        {
            Status = 409, Type = "general/locked",
        });
        var existing = await Context.Database.GetAsync<Problem>(original.Id);
        existing.Should().BeEquivalentTo(original, x => x.Excluding(p => p.AddedAt));
    }
}
