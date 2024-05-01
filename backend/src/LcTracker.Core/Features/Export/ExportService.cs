using System.Text.Json;
using LcTracker.Core.Storage;
using Microsoft.EntityFrameworkCore;

namespace LcTracker.Core.Features.Export;

public class ExportService(IAppDbContext dbContext)
{
    public async Task Export(Guid userId)
    {
        var problems = await dbContext.Problems
            .Where(x => x.AppUserId == userId)
            .Include(x => x.Attempts!.Where(a => a.AppUserId == userId))
            .ToListAsync();

        var json = JsonSerializer.Serialize(problems);
    }
}
