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

        builder.OwnsMany(x => x.Methods, cfg =>
        {
            cfg.HasKey(x => x.Name);
            cfg.Property(x => x.Name).HasMaxLength(30);
            cfg.Property(x => x.Contents).HasMaxLength(5000);
        });
    }
}
