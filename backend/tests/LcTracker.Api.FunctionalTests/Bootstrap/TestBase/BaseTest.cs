using LcTracker.Api.FunctionalTests.Bootstrap.TestBase.Arrange;
using LcTracker.Api.FunctionalTests.Bootstrap.TestBase.Context;
using LcTracker.Api.FunctionalTests.Bootstrap.TestFixture;

namespace LcTracker.Api.FunctionalTests.Bootstrap.TestBase;

public abstract class BaseTest
{
    protected BaseTest(ApiTestFixture fixture)
    {
        var scope = fixture.CreateScopeAsync();

        Context = new(scope.ServiceProvider);
        Client = new(fixture.ApiClient, Context.Require);

        Arrange = CreateFixture.Execute();
    }

    protected Fixture Arrange { get; }

    protected TestClient Client { get; }

    protected TestContext Context { get; }
}
