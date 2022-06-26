using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WhereHasMyTimeGone.API.Domain.Entities;

namespace WhereHasMyTimeGone.API.Infrastructure.Persistence.Configurations;

public class HuddleGroupConfiguration : IEntityTypeConfiguration<HuddleGroup>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<HuddleGroup> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasMany(x => x.HuddleLinks).WithOne(x => x.Group).HasForeignKey(x => x.GroupId).OnDelete(DeleteBehavior.Cascade);
    }
}
