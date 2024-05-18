using LcTracker.Api.FunctionalTests.Bootstrap.TestBase;
using LcTracker.Api.FunctionalTests.Bootstrap.TestFixture;
using LcTracker.Core.Features.Attempts;

namespace LcTracker.Api.FunctionalTests.Features.Attempts;

[Collection(FeaturesCollection.Name)]
public class DeleteAttemptTests(ApiTestFixture fixture) : AttemptTest(fixture)
{
    [Fact]
    public async Task Deletes()
    {
        var now = Context.Time.SetUtcNow(1.January(2024));
        var userId = await Client.RunAsNewUserAsync();
        var problem = await RequireProblem(userId);
        var attempt = await RequireAttempt(problem, userId);

        var result = await Client
            .DeleteAsync<EmptyResponse>($"api/attempts/{attempt.Id}");

        result.Response.Should().BeSuccessful();
        var exists = await Context.Database.ExistsAsync<Attempt>(attempt.Id);
        exists.Should().BeFalse();
    }
}
