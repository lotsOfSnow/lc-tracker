namespace LcTracker.Api.FunctionalTests.Bootstrap;

public record ClientResult<T>(HttpResponseMessage Response, T? Data);

public readonly struct EmptyResponse;
