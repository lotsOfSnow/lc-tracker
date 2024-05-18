using Testcontainers.PostgreSql;

namespace LcTracker.Api.FunctionalTests.Bootstrap.TestFixture;

public class DatabaseTestContainer : IAsyncLifetime
{
    public PostgreSqlContainer Instance { get; } = SetupDatabase();

    public async Task InitializeAsync()
    {
        await Instance.StartAsync();
    }

    public async Task DisposeAsync()
    {
        await Instance.DisposeAsync();
    }

    private static PostgreSqlContainer SetupDatabase() =>
        new PostgreSqlBuilder()
            .WithName($"LcTrackerDb_{Guid.NewGuid()}")
            .WithDatabase("db")
            .WithUsername("dev")
            .WithPassword("pass")
            .WithImage("postgres:16.3")
            .WithCleanUp(true)
            .Build();
}
