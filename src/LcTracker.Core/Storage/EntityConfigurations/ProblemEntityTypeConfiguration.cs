using LcTracker.Core.Features.AppUsers;
using LcTracker.Core.Features.Attempts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LcTracker.Core.Storage.EntityConfigurations;

public class ProblemEntityTypeConfiguration : IEntityTypeConfiguration<Problem>
{

    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Problem> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne<AppUser>()
            .WithMany()
            .HasForeignKey(x => x.AppUserId);
    }
}
