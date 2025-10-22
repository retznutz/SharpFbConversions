using System.Text.Json.Serialization;

namespace SharpFbConversions.Models;

/// <summary>
/// Response from Facebook App Events API
/// </summary>
public class AppEventResponse
{
    /// <summary>
    /// Number of events received
    /// </summary>
    [JsonPropertyName("events_received")]
    public int EventsReceived { get; set; }

    /// <summary>
    /// Number of events dropped
    /// </summary>
    [JsonPropertyName("events_dropped")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? EventsDropped { get; set; }

    /// <summary>
    /// Facebook trace ID for debugging
    /// </summary>
    [JsonPropertyName("fbtrace_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? FbTraceId { get; set; }

    /// <summary>
    /// Messages from Facebook (warnings or info)
    /// </summary>
    [JsonPropertyName("messages")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<string>? Messages { get; set; }

    /// <summary>
    /// Error information if the request failed
    /// </summary>
    [JsonPropertyName("error")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public FacebookError? Error { get; set; }
}
