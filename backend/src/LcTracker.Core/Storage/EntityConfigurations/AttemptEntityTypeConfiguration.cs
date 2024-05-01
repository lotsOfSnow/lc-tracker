using LcTracker.Core.Features.AppUsers;
using LcTracker.Core.Features.Attempts;
using LcTracker.Core.Features.Problems;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LcTracker.Core.Storage.EntityConfigurations;

public class AttemptEntityTypeConfiguration : IEntityTypeConfiguration<Attempt>
{
    public void Configure(EntityTypeBuilder<Attempt> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne<Problem>()
            .WithMany(x => x.Attempts)
            .HasForeignKey(x => x.ProblemId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.Note)
            .HasMaxLength(50000);

        builder.HasOne<AppUser>()
            .WithMany()
            .HasForeignKey(x => x.AppUserId);
    }
}
