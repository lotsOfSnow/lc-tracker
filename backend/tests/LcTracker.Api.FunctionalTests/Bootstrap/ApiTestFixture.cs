using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;

namespace LcTracker.Api.FunctionalTests.Bootstrap;

[UsedImplicitly]
public class ApiTestFixture : IAsyncLifetime
{
    private TestApiFactory _app = null!;

    public AsyncServiceScope CreateScopeAsync() => _app.Services.CreateAsyncScope();

    public HttpClient ApiClient { get; private set; } = null!;
    public async Task InitializeAsync()
    {
        await Task.CompletedTask;

        _app = new($"DataSource=file::memory:?cache=shared");

        ApiClient = _app.CreateClient();
    }

    public async Task DisposeAsync()
    {
        await _app.DisposeAsync();
    }
}
