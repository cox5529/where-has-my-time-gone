namespace WhereHasMyTimeGone.API.Domain.Entities;

public class Huddle
{
    public Guid Id { get; set; }    
    
    public Guid UserProfileId { get; set; }
    
    public DateTime Start { get; set; }
    
    public DateTime? End { get; set; }
    
    public UserProfile? UserProfile { get; set; }

    public virtual ICollection<HuddleLink> HuddleLinks { get; set; } = new List<HuddleLink>();
}
