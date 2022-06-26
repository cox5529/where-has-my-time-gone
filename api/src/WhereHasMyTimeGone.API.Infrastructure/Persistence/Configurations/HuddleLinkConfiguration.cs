using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WhereHasMyTimeGone.API.Domain.Entities;

namespace WhereHasMyTimeGone.API.Infrastructure.Persistence.Configurations;

public class HuddleLinkConfiguration : IEntityTypeConfiguration<HuddleLink>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<HuddleLink> builder)
    {
        builder.HasKey(
            x => new
            {
                x.GroupId,
                x.HuddleId
            });
    }
}
