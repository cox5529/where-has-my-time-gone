using Newtonsoft.Json;

namespace WhereHasMyTimeGone.API.Application.Common.Models.Slack;

public class SlackProfile
{
    public string? Title { get; set; }
    
    public string? Phone { get; set; }
    
    public string? Skype { get; set; }
    
    [JsonProperty(PropertyName = "real_name")]
    public string? RealName { get; set; }
    
    [JsonProperty(PropertyName = "real_name_normalized")]
    public string? RealNameNormalized { get; set; }
    
    [JsonProperty(PropertyName = "display_name")]
    public string? DisplayName { get; set; }
    
    [JsonProperty(PropertyName = "display_name_normalized")]
    public string? DisplayNameNormalized { get; set; }
    
    [JsonProperty(PropertyName = "status_text")]
    public string? StatusText { get; set; }
    
    [JsonProperty(PropertyName = "status_emoji")]
    public string? StatusEmoji { get; set; }
    
    [JsonProperty(PropertyName = "status_expiration")]
    public long StatusExpiration { get; set; }
    
    [JsonProperty(PropertyName = "avatar_hash")]
    public string? AvatarHash { get; set; }
    
    [JsonProperty(PropertyName = "image_original")]
    public string? ImageOriginal { get; set; }
    
    [JsonProperty(PropertyName = "is_custom_image")]
    public bool IsCustomImage { get; set; }
    
    [JsonProperty(PropertyName = "huddle_state")]
    public string? HuddleState { get; set; }
    
    [JsonProperty(PropertyName = "huddle_state_expiration_ts")]
    public long HuddleStateExpirationTs { get; set; }
    
    [JsonProperty(PropertyName = "first_name")]
    public string? FirstName { get; set; }
    
    [JsonProperty(PropertyName = "last_name")]
    public string? LastName { get; set; }
    
    [JsonProperty(PropertyName = "image_24")]
    public string? Image24 { get; set; }
    
    [JsonProperty(PropertyName = "image_32")]
    public string? Image32 { get; set; }
    
    [JsonProperty(PropertyName = "image_48")]
    public string? Image48 { get; set; }
    
    [JsonProperty(PropertyName = "image_72")]
    public string? Image72 { get; set; }
    
    [JsonProperty(PropertyName = "image_192")]
    public string? Image192 { get; set; }
    
    [JsonProperty(PropertyName = "image_512")]
    public string? Image512 { get; set; }
    
    [JsonProperty(PropertyName = "image_1024")]
    public string? Image1024 { get; set; }
    
    [JsonProperty(PropertyName = "status_text_canonical")]
    public string? StatusTextCanonical { get; set; }
    
    public string? Email { get; set; }
    
    public string? Team { get; set; }
}
