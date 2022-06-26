using Newtonsoft.Json;

namespace WhereHasMyTimeGone.API.Application.Common.Models.Slack;

public class SlackEventBase
{
    public string? Type { get; set; }
}

public class SlackEvent : SlackEventBase
{
    public string? Token { get; set; }

    [JsonProperty(PropertyName = "team_id")]
    public string? TeamId { get; set; }

    [JsonProperty(PropertyName = "api_app_id")]
    public string? ApiAppId { get; set; }

    [JsonProperty(PropertyName = "event_id")]
    public string? EventId { get; set; }

    [JsonProperty(PropertyName = "event_time")]
    public long EventTime { get; set; }

    [JsonProperty(PropertyName = "is_ext_shared_channel")]
    public bool IsExternalSharedChannel { get; set; }
}

public class InnerSlackEvent : SlackEventBase
{
    [JsonProperty(PropertyName = "cache_ts")]
    public long CacheTimestamp { get; set; }
    
    [JsonProperty(PropertyName = "event_ts")]
    public string? EventTimestamp { get; set; }
}

public class SlackEvent<T> : SlackEvent where T : SlackEventBase
{
    public T? Event { get; set; }
}
