using System.Text.Json.Serialization;

namespace SharpFbConversions.Models;

/// <summary>
/// User data associated with an app event. Used for matching and attribution.
/// Fields should be hashed using SHA256 before sending.
/// </summary>
public class UserData
{
    /// <summary>
    /// Email address (should be hashed with SHA256)
    /// </summary>
    [JsonPropertyName("em")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Email { get; set; }

    /// <summary>
    /// First name (should be hashed with SHA256)
    /// </summary>
    [JsonPropertyName("fn")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? FirstName { get; set; }

    /// <summary>
    /// Last name (should be hashed with SHA256)
    /// </summary>
    [JsonPropertyName("ln")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? LastName { get; set; }

    /// <summary>
    /// Phone number (should be hashed with SHA256)
    /// </summary>
    [JsonPropertyName("ph")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Phone { get; set; }

    /// <summary>
    /// Gender (should be hashed with SHA256)
    /// </summary>
    [JsonPropertyName("ge")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Gender { get; set; }

    /// <summary>
    /// Date of birth (should be hashed with SHA256, format YYYYMMDD)
    /// </summary>
    [JsonPropertyName("db")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? DateOfBirth { get; set; }

    /// <summary>
    /// City (should be hashed with SHA256)
    /// </summary>
    [JsonPropertyName("ct")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? City { get; set; }

    /// <summary>
    /// State (should be hashed with SHA256)
    /// </summary>
    [JsonPropertyName("st")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? State { get; set; }

    /// <summary>
    /// Zip code (should be hashed with SHA256)
    /// </summary>
    [JsonPropertyName("zp")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ZipCode { get; set; }

    /// <summary>
    /// Country (should be hashed with SHA256, ISO 3166-1 alpha-2 country code)
    /// </summary>
    [JsonPropertyName("country")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Country { get; set; }

    /// <summary>
    /// External ID
    /// </summary>
    [JsonPropertyName("external_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ExternalId { get; set; }

    /// <summary>
    /// Client IP address
    /// </summary>
    [JsonPropertyName("client_ip_address")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ClientIpAddress { get; set; }

    /// <summary>
    /// Client user agent
    /// </summary>
    [JsonPropertyName("client_user_agent")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ClientUserAgent { get; set; }

    /// <summary>
    /// Facebook Click ID
    /// </summary>
    [JsonPropertyName("fbc")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? FacebookClickId { get; set; }

    /// <summary>
    /// Facebook Browser ID
    /// </summary>
    [JsonPropertyName("fbp")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? FacebookBrowserId { get; set; }

    /// <summary>
    /// Subscription ID
    /// </summary>
    [JsonPropertyName("subscription_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? SubscriptionId { get; set; }

    /// <summary>
    /// Facebook Login ID
    /// </summary>
    [JsonPropertyName("fb_login_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? FacebookLoginId { get; set; }

    /// <summary>
    /// Lead ID
    /// </summary>
    [JsonPropertyName("lead_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? LeadId { get; set; }

    /// <summary>
    /// Advertiser ID
    /// </summary>
    [JsonPropertyName("madid")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? AdvertiserId { get; set; }

    /// <summary>
    /// Android Advertising ID
    /// </summary>
    [JsonPropertyName("anon_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? AndroidAdvertisingId { get; set; }
}
