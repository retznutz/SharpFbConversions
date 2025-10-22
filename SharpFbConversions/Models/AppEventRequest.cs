using System.Text.Json.Serialization;

namespace SharpFbConversions.Models;

/// <summary>
/// Request payload for sending app events to Facebook
/// </summary>
public class AppEventRequest
{
    /// <summary>
    /// Array of app events to send
    /// </summary>
    [JsonPropertyName("data")]
    public List<AppEvent> Data { get; set; } = new();

    /// <summary>
    /// Test event code for testing events without affecting live data
    /// </summary>
    [JsonPropertyName("test_event_code")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? TestEventCode { get; set; }

    /// <summary>
    /// Partner agent string to identify the integration
    /// </summary>
    [JsonPropertyName("partner_agent")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? PartnerAgent { get; set; }

    /// <summary>
    /// Namespace ID for custom events
    /// </summary>
    [JsonPropertyName("namespace_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? NamespaceId { get; set; }

    /// <summary>
    /// Upload ID for tracking
    /// </summary>
    [JsonPropertyName("upload_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? UploadId { get; set; }

    /// <summary>
    /// Upload tag for categorizing uploads
    /// </summary>
    [JsonPropertyName("upload_tag")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? UploadTag { get; set; }

    /// <summary>
    /// Upload source
    /// </summary>
    [JsonPropertyName("upload_source")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? UploadSource { get; set; }
}
