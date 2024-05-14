using LcTracker.Api.FunctionalTests.Bootstrap.Context;

namespace LcTracker.Api.FunctionalTests.Bootstrap;

public abstract class BaseTest
{
    protected BaseTest(ApiTestFixture fixture)
    {
        var scope = fixture.CreateScopeAsync();

        Context = new(scope.ServiceProvider);
        Client = new(fixture.ApiClient, Context.Require);

        var arrange = new Fixture();
        arrange.Customize<DateOnly>(composer => composer.FromFactory<DateTime>(DateOnly.FromDateTime));
        Arrange = arrange;
    }

    protected Fixture Arrange { get; }

    protected TestClient Client { get; }

    protected TestContext Context { get; }
}
