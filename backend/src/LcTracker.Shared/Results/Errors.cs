namespace LcTracker.Shared.Results;

public static class Errors
{
    public const string NotFoundCode = "not-found";
    public static readonly ResultError NotFound = new(new(NotFoundCode));
}
