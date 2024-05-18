namespace LcTracker.Core.Features.Problems;

public class ProblemMethod
{
    public string Name { get; set; }

    public string Contents { get; set; }

    public ProblemMethod(string name, string contents)
    {
        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(contents))
        {
            throw new("Method can't be empty");
        }

        Name = name.Trim();
        Contents = contents.Trim();
    }
}
