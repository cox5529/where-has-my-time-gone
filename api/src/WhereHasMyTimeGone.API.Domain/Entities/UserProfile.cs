using WhereHasMyTimeGone.API.Domain.Common;

namespace WhereHasMyTimeGone.API.Domain.Entities;

public class UserProfile : IBaseEntity<Guid>
{
    public Guid Id { get; set; }

    public bool Disabled { get; set; }

    public string? UserId { get; set; }
    
    public string? DisplayName { get; set; }

    public string Email { get; set; } = "";
    
    public string? ProfileImage { get; set; }

    public virtual ICollection<Person> People { get; set; } = new List<Person>();

    public virtual ICollection<Huddle> Huddles { get; set; } = new List<Huddle>();

    public virtual ICollection<HuddleGroup> HuddleGroups { get; set; } = new List<HuddleGroup>();
}
