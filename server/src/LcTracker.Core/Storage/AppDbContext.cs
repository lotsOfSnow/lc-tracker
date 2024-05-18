using EntityFramework.Exceptions.PostgreSQL;
using LcTracker.Core.Features.AppUsers;
using LcTracker.Core.Features.Attempts;
using LcTracker.Core.Features.Problems;
using LcTracker.Core.Storage.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace LcTracker.Core.Storage;

public sealed class AppDbContext(DbContextOptions<AppDbContext> dbContextOptions)
    : DbContext(dbContextOptions), IAppDbContext
{
    public DbSet<AppUser> AppUsers { get; init; } = null!;

    public DbSet<Attempt> Attempts { get; init; } = null!;

    public DbSet<Problem> Problems { get; init; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(AppUserEntityTypeConfiguration).Assembly);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseExceptionProcessor();
    }
}
