using WhereHasMyTimeGone.API.Domain.Common;
using WhereHasMyTimeGone.API.Domain.Enums;

namespace WhereHasMyTimeGone.API.Domain.Entities;

public class TimeUsage : IBaseEntity<int>
{
    public int Id { get; set; }

    public bool Disabled { get; set; }

    public DateTime Start { get; set; }

    public DateTime End { get; set; }

    public TimeSpan Duration => End - Start;

    public TimeUsageType Type { get; set; }

    public int PersonId { get; set; }
    
    public Person? Person { get; set; }
}
