using LcTracker.Api.FunctionalTests.Bootstrap.Fakes;
using LcTracker.Core.Auth;
using LcTracker.Core.Storage;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Time.Testing;
using WebMotions.Fake.Authentication.JwtBearer;

namespace LcTracker.Api.FunctionalTests.Bootstrap;

public class TestApiFactory(string databaseConnectionString) : WebApplicationFactory<Program>
{
    private const string EnvironmentName = "test";

    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.ConfigureHostConfiguration(
            config =>
            {
                config.AddJsonFile($"appsettings.{EnvironmentName}.json");
                config.AddInMemoryCollection(new[]
                {
                    new KeyValuePair<string, string?>(
                        $"{DatabaseOptions.SectionName}:{nameof(DatabaseOptions.ConnectionString)}",
                        databaseConnectionString),
                });
            });

        return base.CreateHost(builder);
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment(EnvironmentName);

        builder.ConfigureTestServices(
            services =>
            {
                services.AddAuthentication(FakeJwtBearerDefaults.AuthenticationScheme)
                    .AddFakeJwtBearer();

                services.AddSingleton<TimeProvider, FakeTimeProvider>();
                services.AddTransient<IGetCurrentUserId, FakeGetCurrentUserId>();
            });
    }

}
