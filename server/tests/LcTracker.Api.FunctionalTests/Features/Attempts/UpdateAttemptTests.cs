using LcTracker.Api.FunctionalTests.Bootstrap.TestBase;
using LcTracker.Api.FunctionalTests.Bootstrap.TestFixture;
using LcTracker.Core.Features.Attempts;
using LcTracker.Core.Features.Attempts.Commands;
using LcTracker.Core.Features.Problems;
using Microsoft.AspNetCore.Mvc;

namespace LcTracker.Api.FunctionalTests.Features.Attempts;

[Collection(FeaturesCollection.Name)]
public class UpdateAttemptTests(ApiTestFixture fixture) : AttemptTest(fixture)
{
    [Fact]
    public async Task Updates()
    {
        var now = Context.Time.SetUtcNow(1.January(2024));
        var userId = await Client.RunAsNewUserAsync();
        var attempt = await RequireAttempt(userId);
        var request = GetRequest(attempt.ProblemId);

        var result = await Client
            .PutAsync<EmptyResponse>($"api/attempts/{attempt.Id}", request);

        result.Response.Should().BeSuccessful();
        var data = await Context.Database.GetAsync<Attempt>(attempt.Id);
        data.Should().BeEquivalentTo(request);
    }

    [Fact]
    public async Task Disallows_updating_locked()
    {
        var userId = await Client.RunAsNewUserAsync();
        var problem = await RequireProblem(userId);
        var original = await Context.Require.Object(
            Arrange
                .BuildWithUserId<Attempt>(userId)
                .With(x => x.ProblemId, problem.Id)
                .With(x => x.IsLocked, true)
                .Create());
        var request = GetRequest(problem.Id);

        var result = await Client.PutAsync<ProblemDetails>($"api/attempts/{original.Id}", request);

        result.Response.Should().HaveError();
        result.Data.Should().BeEquivalentTo(new
        {
            Status = 409, Type = "general/locked",
        });
        var existing = await Context.Database.GetAsync<Attempt>(original.Id);
        existing.Should().BeEquivalentTo(original);
    }

    private UpdateAttemptRequest GetRequest(Guid problemId)
    {
        var now = Context.Time.SetUtcNow(1.January(2024));

        return Arrange
            .Build<UpdateAttemptRequest>()
            .With(x => x.ProblemId, problemId)
            .With(x => x.Date, DateOnly.FromDateTime(now.AddDays(-1)))
            .Create();
    }
}
