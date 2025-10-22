using System.Text.Json.Serialization;

namespace SharpFbConversions.Models;

/// <summary>
/// Facebook API error details
/// </summary>
public class FacebookError
{
    /// <summary>
    /// Error message
    /// </summary>
    [JsonPropertyName("message")]
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Error type
    /// </summary>
    [JsonPropertyName("type")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Type { get; set; }

    /// <summary>
    /// Error code
    /// </summary>
    [JsonPropertyName("code")]
    public int Code { get; set; }

    /// <summary>
    /// Error subcode
    /// </summary>
    [JsonPropertyName("error_subcode")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? ErrorSubcode { get; set; }

    /// <summary>
    /// Facebook trace ID
    /// </summary>
    [JsonPropertyName("fbtrace_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? FbTraceId { get; set; }

    /// <summary>
    /// Is transient error (can be retried)
    /// </summary>
    [JsonPropertyName("is_transient")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? IsTransient { get; set; }

    /// <summary>
    /// Error user title
    /// </summary>
    [JsonPropertyName("error_user_title")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ErrorUserTitle { get; set; }

    /// <summary>
    /// Error user message
    /// </summary>
    [JsonPropertyName("error_user_msg")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ErrorUserMsg { get; set; }
}
