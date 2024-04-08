using System.ComponentModel.DataAnnotations;

namespace LcTracker.Core.Storage;

public class DatabaseOptions
{
    public const string SectionName = "Database";

    [Required(AllowEmptyStrings = false)]
    public required string ConnectionString { get; init; }
}
