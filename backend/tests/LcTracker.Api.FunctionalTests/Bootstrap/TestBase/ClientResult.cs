namespace LcTracker.Api.FunctionalTests.Bootstrap.TestBase;

public record ClientResult<T>(HttpResponseMessage Response, ClientResultData<T> ParsedData)
{
    public T? Data => ParsedData.Value;
}

public record ClientResultData<T>(T? Value, string? Raw);

public readonly struct EmptyResponse;
