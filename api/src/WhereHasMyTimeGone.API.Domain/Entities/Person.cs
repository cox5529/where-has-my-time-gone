using WhereHasMyTimeGone.API.Domain.Common;

namespace WhereHasMyTimeGone.API.Domain.Entities;

public class Person : IBaseEntity<int>
{
    public int Id { get; set; }

    public bool Disabled { get; set; }

    public string Name { get; set; } = "";

    public string? ImageUrl { get; set; }

    public Guid UserProfileId { get; set; }

    public UserProfile? UserProfile { get; set; }

    public virtual ICollection<TimeUsage> Usages { get; set; } = new List<TimeUsage>();
}
