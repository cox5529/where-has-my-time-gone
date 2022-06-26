namespace WhereHasMyTimeGone.API.Domain.Entities;

public class HuddleGroup
{
    public Guid Id { get; set; }
    
    public Guid OwnerId { get; set; }
    
    public UserProfile? Owner { get; set; }

    public virtual ICollection<HuddleLink> HuddleLinks { get; set; } = new List<HuddleLink>();
}
