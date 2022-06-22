using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WhereHasMyTimeGone.API.Domain.Entities;

namespace WhereHasMyTimeGone.API.Infrastructure.Persistence.Configurations;

public class UserProfileConfiguration : IEntityTypeConfiguration<UserProfile>
{
    public void Configure(EntityTypeBuilder<UserProfile> builder)
    {
        builder.HasIndex(x => x.UserId);
        builder.HasKey(x => x.Id);

        builder.HasMany(x => x.People)
               .WithOne(x => x.UserProfile)
               .HasForeignKey(x => x.UserProfileId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
