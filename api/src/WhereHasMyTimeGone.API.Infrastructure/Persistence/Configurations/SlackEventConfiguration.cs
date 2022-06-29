using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WhereHasMyTimeGone.API.Domain.Entities;

namespace WhereHasMyTimeGone.API.Infrastructure.Persistence.Configurations;

public class SlackEventConfiguration : IEntityTypeConfiguration<SlackEvent>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<SlackEvent> builder)
    {
        builder.HasKey(x => x.Id);
    }
}
