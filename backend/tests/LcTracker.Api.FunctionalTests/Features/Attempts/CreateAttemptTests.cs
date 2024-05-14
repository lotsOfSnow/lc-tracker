using LcTracker.Core.Features.Attempts;
using LcTracker.Core.Features.Attempts.Commands;

namespace LcTracker.Api.FunctionalTests.Features.Attempts;

[Collection(FeaturesCollection.Name)]
public class CreateAttemptTests(ApiTestFixture fixture) : AttemptTest(fixture)
{
    [Fact]
    public async Task Creates()
    {
        var now = Context.Time.SetUtcNow(1.January(2024));
        var userId = await Client.RunAsNewUserAsync();
        var problem = await RequireProblem(userId);
        var request = Arrange
            .Build<CreateAttemptRequest>()
            .With(x => x.ProblemId, problem.Id)
            .With(x => x.Date, DateOnly.FromDateTime(now.AddDays(-1).DateTime))
            .Create();

        var result = await Client
            .PostAsync<CreateAttemptResponse>("api/attempts", request);

        result.Response.Should().BeSuccessful();
        var data = await Context.Database.GetAsync<Attempt>(result.Data!.Id);
        data.Note.Should().Be(request.Note);
        data.AppUserId.Should().Be(userId);
        data.ProblemId.Should().Be(problem.Id);
        data.Date.Should().Be(request.Date);
        data.Difficulty.Should().Be(request.Difficulty);
        data.HasSolved.Should().Be(request.HasSolved);
        data.HasUsedHelp.Should().Be(request.HasUsedHelp);
        data.MinutesSpent.Should().Be(request.MinutesSpent);
        data.IsRecap.Should().Be(request.IsRecap);
    }
}
