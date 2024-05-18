using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;

namespace LcTracker.Api.FunctionalTests.Bootstrap.TestFixture;

[UsedImplicitly]
public class ApiTestFixture : IAsyncLifetime
{
    private readonly DatabaseTestContainer _dbContainer = new();

    private TestApiFactory _app = null!;

    public AsyncServiceScope CreateScopeAsync() => _app.Services.CreateAsyncScope();

    public HttpClient ApiClient { get; private set; } = null!;
    public async Task InitializeAsync()
    {
        await _dbContainer.InitializeAsync();
        _app = new(_dbContainer.Instance.GetConnectionString());

        ApiClient = _app.CreateClient();
    }

    public async Task DisposeAsync()
    {
        await _app.DisposeAsync();
        await _dbContainer.DisposeAsync();
    }
}
