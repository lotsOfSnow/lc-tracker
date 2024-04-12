using LcTracker.Core.Features.AppUsers;
using LcTracker.Core.Features.Problems;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LcTracker.Core.Storage.EntityConfigurations;

public class ProblemEntityTypeConfiguration : IEntityTypeConfiguration<Problem>
{
    public void Configure(EntityTypeBuilder<Problem> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne<AppUser>()
            .WithMany()
            .HasForeignKey(x => x.AppUserId);

        builder.HasIndex(x => x.Number).IsUnique();
    }
}
