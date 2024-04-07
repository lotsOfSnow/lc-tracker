using LcTracker.Core.Storage.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace LcTracker.Core.Storage;

public sealed class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions) : base(dbContextOptions)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(AppUserEntityTypeConfiguration).Assembly);
    }
}
