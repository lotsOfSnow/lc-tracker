using System.ComponentModel.DataAnnotations;

namespace LcTracker.Core.Features.LeetCode;

public class LeetCodeApiOptions
{
    public const string SectionName = "LeetCodeApi";

    [Required(AllowEmptyStrings = false)]
    public required Uri GraphQlEndpoint { get; init; }
}
