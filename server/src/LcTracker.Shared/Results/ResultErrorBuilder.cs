namespace LcTracker.Shared.Results;

public class ResultErrorBuilder(ErrorCode code)
{
    private readonly List<string> _messages = [];

    public ResultErrorBuilder Because(string message)
    {
        _messages.Add(message);

        return this;
    }

    public ResultError Create() => new(code, _messages.ToArray());
}
