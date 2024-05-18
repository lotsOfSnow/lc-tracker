using LcTracker.Shared.Results;

namespace LcTracker.Core.Features.Problems;

public static class ProblemErrorCodes
{
    private static ErrorCode GetCode(string value) => new("Problems", value);
}
