using System.Text.Json.Serialization;

namespace SharpFbConversions.Models;

/// <summary>
/// App-specific data for the event
/// </summary>
public class AppData
{
    /// <summary>
    /// Application tracking enabled (0 or 1). Indicates whether the user has opted into tracking.
    /// </summary>
    [JsonPropertyName("application_tracking_enabled")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? ApplicationTrackingEnabled { get; set; }

    /// <summary>
    /// Advertiser tracking enabled (0 or 1). iOS 14+ tracking transparency indicator.
    /// </summary>
    [JsonPropertyName("advertiser_tracking_enabled")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? AdvertiserTrackingEnabled { get; set; }

    /// <summary>
    /// Consider views (0 or 1)
    /// </summary>
    [JsonPropertyName("consider_views")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? ConsiderViews { get; set; }

    /// <summary>
    /// Device token for push notifications
    /// </summary>
    [JsonPropertyName("device_token")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? DeviceToken { get; set; }

    /// <summary>
    /// Include dwell data (0 or 1)
    /// </summary>
    [JsonPropertyName("include_dwell_data")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? IncludeDwellData { get; set; }

    /// <summary>
    /// Include video data (0 or 1)
    /// </summary>
    [JsonPropertyName("include_video_data")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? IncludeVideoData { get; set; }

    /// <summary>
    /// Install referrer
    /// </summary>
    [JsonPropertyName("install_referrer")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? InstallReferrer { get; set; }

    /// <summary>
    /// Installer package
    /// </summary>
    [JsonPropertyName("installer_package")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? InstallerPackage { get; set; }

    /// <summary>
    /// Receipt data (for iOS in-app purchases)
    /// </summary>
    [JsonPropertyName("receipt_data")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ReceiptData { get; set; }

    /// <summary>
    /// URL schemes
    /// </summary>
    [JsonPropertyName("url_schemes")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<string>? UrlSchemes { get; set; }

    /// <summary>
    /// Windows attribution ID
    /// </summary>
    [JsonPropertyName("windows_attribution_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? WindowsAttributionId { get; set; }

    /// <summary>
    /// Campaign IDs
    /// </summary>
    [JsonPropertyName("campaign_ids")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? CampaignIds { get; set; }

    /// <summary>
    /// Additional app data properties
    /// </summary>
    [JsonExtensionData]
    public Dictionary<string, object>? AdditionalProperties { get; set; }
}
