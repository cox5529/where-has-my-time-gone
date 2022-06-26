using Newtonsoft.Json;

namespace WhereHasMyTimeGone.API.Application.Common.Models.Slack;

public class SlackUser
{
    public string? Id { get; set; }
    
    [JsonProperty(PropertyName = "team_id")]
    public string? TeamId { get; set; }
    
    public string? Name { get; set; }
    
    public bool Deleted { get; set; }
    
    public string? Color { get; set; }
    
    [JsonProperty(PropertyName = "real_name")]
    public string? RealName { get; set; }
    
    [JsonProperty(PropertyName = "tz")]
    public string? Timezone { get; set; }
    
    [JsonProperty(PropertyName = "tz_offset")]
    public int TimezoneOffset { get; set; }
    
    [JsonProperty(PropertyName = "is_admin")]
    public bool IsAdmin { get; set; }
    
    [JsonProperty(PropertyName = "is_owner")]
    public bool IsOwner { get; set; }
    
    [JsonProperty(PropertyName = "is_primary_owner")]
    public bool IsPrimaryOwner { get; set; }
    
    [JsonProperty(PropertyName = "is_restricted")]
    public bool IsRestricted { get; set; }
    
    [JsonProperty(PropertyName = "is_ultra_restricted")]
    public bool IsUltraRestricted { get; set; }
    
    [JsonProperty(PropertyName = "is_bot")]
    public bool IsBot { get; set; }
    
    [JsonProperty(PropertyName = "is_app_user")]
    public bool IsAppUser { get; set; }
    
    public long Updated { get; set; }
    
    [JsonProperty(PropertyName = "is_email_confirmed")]
    public bool IsEmailConfirmed { get; set; }
    
    [JsonProperty(PropertyName = "who_can_share_contact_card")]
    public string? WhoCanShareContactCard { get; set; }
    
    [JsonProperty(PropertyName = "locale")]
    public string? Locale { get; set; }
    
    public SlackProfile? Profile { get; set; }
}
