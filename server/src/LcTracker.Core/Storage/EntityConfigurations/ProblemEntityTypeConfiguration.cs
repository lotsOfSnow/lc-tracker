using LcTracker.Core.Features.AppUsers;
using LcTracker.Core.Features.Problems;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LcTracker.Core.Storage.EntityConfigurations;

public class ProblemEntityTypeConfiguration : IEntityTypeConfiguration<Problem>
{
    private const int MaxMethodNameLength = 40;

    public void Configure(EntityTypeBuilder<Problem> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne<AppUser>()
            .WithMany()
            .HasForeignKey(x => x.AppUserId);

        builder.HasIndex(x => x.Title).IsUnique();

        builder.Property(x => x.Title).IsRequired();

        builder.Property(x => x.Title).HasMaxLength(50);
        builder.Property(x => x.Slug).HasMaxLength(60);
        builder.Property(x => x.Note).HasMaxLength(1000);

        builder.OwnsMany(x => x.Methods, cfg =>
        {
            cfg.HasKey(x => x.Name);
            cfg.Property(x => x.Name).HasMaxLength(MaxMethodNameLength);
            cfg.Property(x => x.Contents).HasMaxLength(5000);
        });
    }
}
