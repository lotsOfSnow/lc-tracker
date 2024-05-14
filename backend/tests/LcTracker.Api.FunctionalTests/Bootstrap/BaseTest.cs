using LcTracker.Api.FunctionalTests.Bootstrap.Context;

namespace LcTracker.Api.FunctionalTests.Bootstrap;

public abstract class BaseTest
{
    protected BaseTest(ApiTestFixture fixture)
    {
        var scope = fixture.CreateScopeAsync();

        Fixture = fixture;
        Context = new(scope.ServiceProvider);
        Client = new(fixture.ApiClient, Context.PrerequisiteData);
    }

    protected TestClient Client { get; }

    protected ApiTestFixture Fixture { get; }

    protected TestContext Context { get; }
}
