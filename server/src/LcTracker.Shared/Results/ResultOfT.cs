using System.Diagnostics.CodeAnalysis;

namespace LcTracker.Shared.Results;

public class Result<T>
{
    private readonly T? _value;
    private readonly ResultError? _error;

    private Result(T value)
    {
        _value = value ?? throw new ArgumentNullException(nameof(value));
        IsSuccess = true;
        _error = null;
    }

    private Result(ResultError error)
    {
        _value = default;
        IsSuccess = false;
        _error = error ?? throw new ArgumentNullException(nameof(error));
    }

    [MemberNotNullWhen(true, nameof(Value))]
    [MemberNotNullWhen(false, nameof(Error))]
    public bool IsSuccess { get; }

    [MemberNotNullWhen(false, nameof(Value))]
    [MemberNotNullWhen(true, nameof(Error))]
    public bool Failed => !IsSuccess;

    public T? Value => IsSuccess
        ? _value
        : throw new InvalidOperationException("Trying to access value of failed result");

    public ResultError? Error => !IsSuccess
        ? _error
        : throw new InvalidOperationException("Trying to access error of successful result");

    public static Result<T> Fail(
        ResultError error) => new(error);

    public static Result<T> Ok(
        T result) => new(result);

    public static implicit operator Result<T>(T value)
        => Ok(value);

    public static implicit operator Result<T>(ResultError error)
        => Fail(error);
}
