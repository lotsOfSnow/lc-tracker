using LcTracker.Core.Features.AppUsers;
using LcTracker.Core.Features.Attempts;
using LcTracker.Core.Features.Problems;
using Microsoft.EntityFrameworkCore;

namespace LcTracker.Core.Storage;

public interface IAppDbContext
{
    public DbSet<AppUser> AppUsers { get; }

    public DbSet<Attempt> Attempts { get; init; }

    public DbSet<Problem> Problems { get; init; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
