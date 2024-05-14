using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Http.Json;
using LcTracker.Api.FunctionalTests.Bootstrap.Context;

namespace LcTracker.Api.FunctionalTests.Bootstrap;

public class TestClient(HttpClient client, TestContextPrerequisiteData require)
{
    public async Task<Guid> RunAsNewUserAsync()
    {
        var user = await require.User();

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

        var data = await Parse<TResult>(response);

        return new(response, data);
    }

    public Task<HttpResponseMessage> PutAsync<TValue>(
        [StringSyntax(StringSyntaxAttribute.Uri)] string? requestUri,
        TValue value)
    {
        return client.PutAsJsonAsync(requestUri, value, TestJsonUtils.Options);
    }

    private static Task<T?> Parse<T>(HttpResponseMessage response)
    {
        try
        {
            return response.Content.ReadFromJsonAsync<T>();
        }
        catch
        {
            return Task.FromResult<T?>(default);
        }
    }
}
