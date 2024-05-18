namespace LcTracker.Shared.Results;

public record ErrorCode(string? Group, string Value)
{
    public ErrorCode(string value) : this(null, value)
    {
    }

    public string Group { get; } = (Group ?? "general").ToLowerInvariant();

    public string Value { get; } = Value.ToLowerInvariant();

    public override string ToString() => $"{Group}/{Value}";
}
