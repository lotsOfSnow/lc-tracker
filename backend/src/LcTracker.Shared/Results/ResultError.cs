namespace LcTracker.Shared.Results;

public class ResultError
{
    private readonly List<string> _messages;

    public ResultError(ErrorCode code, params string[] messages)
    {
        Code = code;
        _messages = messages.Distinct().ToList();;
    }

    public ErrorCode Code { get; }

    public IEnumerable<string> Messages => _messages;

    public ResultError Because(string message) => new(Code, message);
}
