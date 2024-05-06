namespace LcTracker.Core.Features.Problems.Contracts;

public record ProblemMethodDto(string Name, string Contents)
{
    public static HashSet<ProblemMethod> Map(IEnumerable<ProblemMethodDto> dtos) =>
        dtos.Select(x => new ProblemMethod(x.Name, x.Contents)).ToHashSet();
}
