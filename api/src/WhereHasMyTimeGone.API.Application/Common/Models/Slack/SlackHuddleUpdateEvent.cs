namespace WhereHasMyTimeGone.API.Application.Common.Models.Slack;

public class SlackHuddleUpdateEvent : InnerSlackEvent
{
    public SlackUser? User { get; set; }
}
