namespace LcTracker.Shared.Results;

public static class Errors
{
    public const string NotFoundCode = "not-found";
    public static readonly ResultErrorBuilder NotFound = new(new(NotFoundCode));
}
