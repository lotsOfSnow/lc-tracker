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

    public async Task<ClientResult<TResult>> PostAsync<TResult>(
        [StringSyntax(StringSyntaxAttribute.Uri)] string? requestUri,
        object body)
    {
        var response = await client.PostAsJsonAsync(requestUri, body, TestJsonUtils.Options);

        TResult? data = default;
        if (response.IsSuccessStatusCode)
        {
            data = await response.Content.ReadFromJsonAsync<TResult>();
        }

        return new(response, data);
    }

    public Task<HttpResponseMessage> PutAsync<TValue>(
        [StringSyntax(StringSyntaxAttribute.Uri)] string? requestUri,
        TValue value)
    {
        return client.PutAsJsonAsync(requestUri, value, TestJsonUtils.Options);
    }

}
