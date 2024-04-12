using System.Diagnostics.CodeAnalysis;

namespace LcTracker.Shared.Results;

public class Result
{
    private readonly ResultError? _error;

    private Result(bool isSuccess, ResultError? error = null)
    {
        IsSuccess = isSuccess;
        _error = error;
    }

    [MemberNotNullWhen(false, nameof(Error))]
    public bool IsSuccess { get; }

    public ResultError? Error => !IsSuccess
        ? _error
        : throw new InvalidOperationException("Trying to access error of successful result");

    public static Result Fail(ResultError error) => new(false, error);

    public static Result Ok() => new(true);

    public static implicit operator Result(ResultError error) => Fail(error);
}
