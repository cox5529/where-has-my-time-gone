using WhereHasMyTimeGone.API.Application.Common.Interfaces;

namespace WhereHasMyTimeGone.API.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.UtcNow;
}
