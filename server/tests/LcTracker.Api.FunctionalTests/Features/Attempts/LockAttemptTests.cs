using LcTracker.Api.FunctionalTests.Bootstrap.TestBase;
using LcTracker.Api.FunctionalTests.Bootstrap.TestFixture;
using LcTracker.Core.Features.Attempts;

namespace LcTracker.Api.FunctionalTests.Features.Attempts;

[Collection(FeaturesCollection.Name)]
public class LockAttemptTests(ApiTestFixture fixture) : AttemptTest(fixture)
{
    [Fact]
    public async Task Locks()
    {
        var userId = await Client.RunAsNewUserAsync();
        var attempt = await RequireAttempt(userId);

        var result = await Client
            .PutAsync<EmptyResponse>($"api/attempts/{attempt.Id}/lock");

        result.Response.Should().BeSuccessful();
        var data = await Context.Database.GetAsync<Attempt>(attempt.Id);
        data.IsLocked.Should().BeTrue();
    }

    [Fact]
    public async Task Unlocks()
    {
        var userId = await Client.RunAsNewUserAsync();
        var attempt = await RequireAttempt(userId);
        await Client
            .PutAsync<EmptyResponse>($"api/attempts/{attempt.Id}/lock");

        var result = await Client
            .DeleteAsync<EmptyResponse>($"api/attempts/{attempt.Id}/lock");

        result.Response.Should().BeSuccessful();
        var data = await Context.Database.GetAsync<Attempt>(attempt.Id);
        data.IsLocked.Should().BeFalse();
    }
}
