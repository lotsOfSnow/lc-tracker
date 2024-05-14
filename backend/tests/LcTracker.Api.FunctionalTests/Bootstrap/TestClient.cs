using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Http.Json;
using LcTracker.Api.FunctionalTests.Bootstrap.Context;

namespace LcTracker.Api.FunctionalTests.Bootstrap;

public class TestClient(HttpClient client, TestContextPrerequisiteData prerequisiteDataContext)
{
    public async Task<Guid> RunAsNewUserAsync()
    {
        var user = await prerequisiteDataContext.NeedUser();

        client.SetFakeBearerToken(new
        {
            sub = user.Id.ToString(),
        });

        return user.Id;
    }

    public Task<HttpResponseMessage> PostAsync<TValue>(
        [StringSyntax(StringSyntaxAttribute.Uri)] string? requestUri,
        TValue value)
    {
        return client.PostAsJsonAsync(requestUri, value, TestJsonUtils.Options);
    }

    public Task<HttpResponseMessage> PutAsync<TValue>(
        [StringSyntax(StringSyntaxAttribute.Uri)] string? requestUri,
        TValue value)
    {
        return client.PutAsJsonAsync(requestUri, value, TestJsonUtils.Options);
    }

}
