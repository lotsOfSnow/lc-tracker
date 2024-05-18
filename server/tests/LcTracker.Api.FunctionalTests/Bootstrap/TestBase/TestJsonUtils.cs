using System.Text.Json;

namespace LcTracker.Api.FunctionalTests.Bootstrap.TestBase;

public static class TestJsonUtils
{
    public static JsonSerializerOptions Options { get; } = new()
    {
        PropertyNameCaseInsensitive = true,
    };
}
