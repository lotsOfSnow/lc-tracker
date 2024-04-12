namespace LcTracker.Shared.Results;

public record ErrorCode(string? Group, string Value)
{
    public string? Group { get; init; } = Group?.ToLowerInvariant();

    public string Value { get; init; } = Value.ToLowerInvariant();

    public static implicit operator string(ErrorCode code) => $"{code.Group}/{code.Value}";
}
