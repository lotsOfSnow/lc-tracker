using LcTracker.Api.Converters;
using LcTracker.Api.Exceptions;
using LcTracker.Core.Auth;
using LcTracker.Core.Features.Attempts.Commands;
using LcTracker.Core.Features.LeetCode;
using LcTracker.Core.Features.LeetCode.Functions;
using LcTracker.Core.Storage;
using LcTracker.Shared.Functions;
using LcTracker.Shared.Handlers;
using LcTracker.Shared.Options;
using LcTracker.Shared.Time;
using LcTracker.Shared.Web.Cors;
using Microsoft.Extensions.Options;
using Polly;
using Polly.Extensions.Http;

namespace LcTracker.Api.Bootstrap;

public static class DependenciesInstaller
{
    public static void AddDependencies(this WebApplicationBuilder builder)
    {
        builder.AddShared();
        builder.AddCore();
        builder.AddStorage();
        builder.AddApi();
    }

    public static async Task UseDependenciesAsync(this WebApplication app)
    {
        app.UseExceptionHandler();
        await app.UseStorageAsync();
        app.UseAppCors();

        // TODO
        if (!app.Environment.IsDevelopment())
        {
            app.UseHttpsRedirection();
        }

        app.UseOpenApi();
        app.UseStaticFiles();
        app.MapControllers();
    }

    private static void AddShared(this WebApplicationBuilder builder)
    {
        builder.Services.AddTimeProvider();
        builder.Services.AddTransient<IDispatcher, Dispatcher>();
    }

    private static void AddCore(this WebApplicationBuilder builder)
    {
        builder.Services.AddFunctions([typeof(GetLeetCodeQuestion).Assembly]);
        builder.Services.AddTransient<IGetCurrentUserId, GetCurrentUserId>();
        builder.Services.AddHandlers(typeof(CreateAttemptCommandHandler).Assembly);

        builder.AddGraphQlClients();
    }

    private static void AddApi(this WebApplicationBuilder builder)
    {
        builder.AddOpenApi();
        builder.Services.AddControllers()
            .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter()));
        builder.AddAppCors();
        builder.Services.AddExceptionHandlers();
    }

    private static void AddGraphQlClients(this WebApplicationBuilder builder)
    {
        builder.Services.AddValidatedOptions<LeetCodeApiOptions>(LeetCodeApiOptions.SectionName);

        builder.Services
            .AddLeetCodeClient()
            .ConfigureHttpClient((sp, client) =>
            {
                var options = sp.GetRequiredService<IOptions<LeetCodeApiOptions>>().Value;
                client.BaseAddress = options.GraphQlEndpoint;
            }, cfg =>
            {
                var retryPolicy = HttpPolicyExtensions
                    .HandleTransientHttpError()
                    .WaitAndRetryAsync(3, attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt)));

                cfg.AddPolicyHandler(retryPolicy);
            });
    }
}
