using LcTracker.Api.FunctionalTests.Bootstrap.Context;

namespace LcTracker.Api.FunctionalTests.Bootstrap;

public abstract class BaseTest
{
    protected BaseTest(ApiTestFixture fixture)
    {
        var scope = fixture.CreateScopeAsync();

        Context = new(scope.ServiceProvider);
        Client = new(fixture.ApiClient, Context.PrerequisiteData);
    }

    protected Fixture Arrange { get; } = new();

    protected TestClient Client { get; }

    protected TestContext Context { get; }
}
