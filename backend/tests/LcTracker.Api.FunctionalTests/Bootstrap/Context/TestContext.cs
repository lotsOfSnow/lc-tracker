using Microsoft.Extensions.DependencyInjection;

namespace LcTracker.Api.FunctionalTests.Bootstrap.Context;

public class TestContext
{
    private readonly IServiceProvider _services;

    public TestContext(
        IServiceProvider services)
    {
        _services = services;
        Time = new(_services);
        Database = new(_services);
        Require = new(Database);
    }

    public TestContextTime Time { get; }

    public TestContextDatabase Database { get; }

    public TestContextPrerequisiteData Require { get; }

    public TService Get<TService>()
        where TService : notnull => _services.GetRequiredService<TService>();
}
