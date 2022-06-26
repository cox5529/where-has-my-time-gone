namespace WhereHasMyTimeGone.API.Domain.Entities;

public class HuddleLink
{
    public Guid HuddleId { get; set; }

    public Guid GroupId { get; set; }

    public Huddle? Huddle { get; set; }

    public HuddleGroup? Group { get; set; }
}
