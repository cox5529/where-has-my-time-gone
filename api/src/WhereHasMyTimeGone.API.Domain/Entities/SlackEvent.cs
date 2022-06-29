namespace WhereHasMyTimeGone.API.Domain.Entities;

public class SlackEvent
{
    public int Id { get; set; }
    
    public DateTime Timestamp { get; set; }
    
    public string Type { get; set; }
    
    public string Content { get; set; }
}
