using LcTracker.Api.FunctionalTests.Bootstrap.TestBase;
using LcTracker.Api.FunctionalTests.Bootstrap.TestFixture;
using LcTracker.Core.Features.Problems;
using LcTracker.Core.Features.Problems.Commands;

namespace LcTracker.Api.FunctionalTests.Features.Problems;

[Collection(FeaturesCollection.Name)]
public class CreateProblemTests(ApiTestFixture fixture) : BaseTest(fixture)
{
    [Fact]
    public async Task Creates_with_overriden_slug()
    {
        var now = Context.Time.SetUtcNow(1.January(2024));
        var userId = await Client.RunAsNewUserAsync();
        var slug = Arrange.Create<string>();
        var request = Arrange
            .Build<CreateProblemRequest>()
            .With(x => x.Slug, "!" + slug)
            .Create();

        var result = await Client
            .PostAsync<CreateProblemResponse>("api/problems", request);

        result.Response.Should().BeSuccessful();
        var data = await Context.Database.GetAsync<Problem>(result.Data!.Id);
        data.Note.Should().Be(request.Note);
        data.Methods.Should().BeEquivalentTo(request.Methods);
        data.Slug.Should().BeNull();
        data.Title.Should().Be(slug);
        data.AppUserId.Should().Be(userId);
        data.AddedAt.Should().Be(now);
    }
}
