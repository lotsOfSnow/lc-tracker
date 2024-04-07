using LcTracker.Core.Auth;
using LcTracker.Core.Features.AppUsers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LcTracker.Core.Storage.EntityConfigurations;

public class AppUserEntityTypeConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasData(new AppUser
        {
            Id = GetCurrentUserId.Id,
        });
    }
}
