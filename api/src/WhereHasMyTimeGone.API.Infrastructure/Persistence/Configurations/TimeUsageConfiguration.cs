using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WhereHasMyTimeGone.API.Domain.Entities;

namespace WhereHasMyTimeGone.API.Infrastructure.Persistence.Configurations;

public class TimeUsageConfiguration : IEntityTypeConfiguration<TimeUsage>
{
    public void Configure(EntityTypeBuilder<TimeUsage> builder)
    {
        builder.HasKey(x => x.Id);
    }
}
