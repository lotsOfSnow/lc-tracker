namespace LcTracker.Api.FunctionalTests.Bootstrap.TestBase;

public record ClientResult<T>(HttpResponseMessage Response, T? Data);

public readonly struct EmptyResponse;
