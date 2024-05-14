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

        return await GetResult<TResult>(response);
    }

    public async Task<ClientResult<TResult>> PutAsync<TResult>(
        [StringSyntax(StringSyntaxAttribute.Uri)] string? requestUri,
        object body)
    {
        var response = await client.PutAsJsonAsync(requestUri, body, TestJsonUtils.Options);

        return await GetResult<TResult>(response);
    }

    private static async Task<ClientResult<TValue>> GetResult<TValue>(HttpResponseMessage response)
    {
        var data = await Parse<TValue>(response);

        return new(response, data);
    }

    private static async Task<T?> Parse<T>(HttpResponseMessage response)
    {
        if (typeof(T) == typeof(EmptyResponse))
        {
            var actual = await response.Content.ReadAsStringAsync();
            if (actual != string.Empty)
            {
                throw new($"Expected to get no content in response, but there is data present: '{actual}'");
            }

            return default;
        }

        try
        {
            return await response.Content.ReadFromJsonAsync<T>();
        }
        catch
        {
            return default;
        }
    }
}
