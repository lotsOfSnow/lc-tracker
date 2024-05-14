using LcTracker.Core.Features.Attempts;
using LcTracker.Core.Features.Attempts.Commands;

namespace LcTracker.Api.FunctionalTests.Features.Attempts;

[Collection(FeaturesCollection.Name)]
public class UpdateAttemptTests(ApiTestFixture fixture) : AttemptTest(fixture)
{
    [Fact]
    public async Task Updates()
    {
        var now = Context.Time.SetUtcNow(1.January(2024));
        var userId = await Client.RunAsNewUserAsync();
        var problem = await RequireProblem(userId);
        var attempt = await RequireAttempt(problem, userId, now);
        var request = Arrange
            .Build<UpdateAttemptRequest>()
            .With(x => x.ProblemId, problem.Id)
            .With(x => x.Date, DateOnly.FromDateTime(now.AddDays(-1).DateTime))
            .Create();

        var result = await Client
            .PutAsync<EmptyResponse>($"api/attempts/{attempt.Id}", request);

        result.Response.Should().BeSuccessful();
        var data = await Context.Database.GetAsync<Attempt>(attempt.Id);
        data.Should().BeEquivalentTo(request);
    }
}
