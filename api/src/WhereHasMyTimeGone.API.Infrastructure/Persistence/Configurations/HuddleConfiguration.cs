using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WhereHasMyTimeGone.API.Domain.Entities;

namespace WhereHasMyTimeGone.API.Infrastructure.Persistence.Configurations;

public class HuddleConfiguration : IEntityTypeConfiguration<Huddle>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Huddle> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasMany(b => b.HuddleLinks).WithOne(x => x.Huddle).HasForeignKey(x => x.HuddleId).OnDelete(DeleteBehavior.Cascade);
    }
}
