using System.ComponentModel.DataAnnotations;

namespace LcTracker.Shared.Web.Cors;

public class AppCorsOptions
{
    public const string SectionName = "Cors";

    [Required(AllowEmptyStrings = false)]
    public required string AllowedOrigin { get; init; }
}
