using System.Text.Json.Serialization;

namespace SharpFbConversions.Models;

/// <summary>
/// Represents a single app event to be sent to Facebook
/// </summary>
public class AppEvent
{
    /// <summary>
    /// Event name. Can be a standard event or custom event name.
    /// Standard events: fb_mobile_achievement_unlocked, fb_mobile_activate_app, fb_mobile_add_payment_info, 
    /// fb_mobile_add_to_cart, fb_mobile_add_to_wishlist, fb_mobile_complete_registration, fb_mobile_content_view, 
    /// fb_mobile_initiated_checkout, fb_mobile_level_achieved, fb_mobile_purchase, fb_mobile_rate, 
    /// fb_mobile_search, fb_mobile_spent_credits, fb_mobile_tutorial_completion
    /// </summary>
    [JsonPropertyName("event_name")]
    public string EventName { get; set; } = string.Empty;

    /// <summary>
    /// Unix timestamp (in seconds) when the event occurred
    /// </summary>
    [JsonPropertyName("event_time")]
    public long EventTime { get; set; }

    /// <summary>
    /// Action source (app, website, email, phone_call, chat, physical_store, system_generated, other)
    /// </summary>
    [JsonPropertyName("action_source")]
    public string ActionSource { get; set; } = "app";

    /// <summary>
    /// User data for matching and attribution
    /// </summary>
    [JsonPropertyName("user_data")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public UserData? UserData { get; set; }

    /// <summary>
    /// Custom data associated with the event
    /// </summary>
    [JsonPropertyName("custom_data")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public CustomData? CustomData { get; set; }

    /// <summary>
    /// App-specific data
    /// </summary>
    [JsonPropertyName("app_data")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public AppData? AppData { get; set; }

    /// <summary>
    /// Event ID for deduplication
    /// </summary>
    [JsonPropertyName("event_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? EventId { get; set; }

    /// <summary>
    /// App version
    /// </summary>
    [JsonPropertyName("_app_version")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? AppVersion { get; set; }

    /// <summary>
    /// Device OS version
    /// </summary>
    [JsonPropertyName("_os_version")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? OsVersion { get; set; }

    /// <summary>
    /// Device model
    /// </summary>
    [JsonPropertyName("_deviceModel")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? DeviceModel { get; set; }

    /// <summary>
    /// Device locale (e.g., "en_US")
    /// </summary>
    [JsonPropertyName("_locale")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Locale { get; set; }

    /// <summary>
    /// Timezone (e.g., "America/Los_Angeles")
    /// </summary>
    [JsonPropertyName("_timezone")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Timezone { get; set; }

    /// <summary>
    /// Carrier (mobile network carrier)
    /// </summary>
    [JsonPropertyName("_carrier")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Carrier { get; set; }

    /// <summary>
    /// Device screen width
    /// </summary>
    [JsonPropertyName("_screen_width")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? ScreenWidth { get; set; }

    /// <summary>
    /// Device screen height
    /// </summary>
    [JsonPropertyName("_screen_height")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? ScreenHeight { get; set; }

    /// <summary>
    /// Device screen density
    /// </summary>
    [JsonPropertyName("_screen_density")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public decimal? ScreenDensity { get; set; }

    /// <summary>
    /// CPU cores
    /// </summary>
    [JsonPropertyName("_cpu_cores")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? CpuCores { get; set; }

    /// <summary>
    /// Total disk space in GB
    /// </summary>
    [JsonPropertyName("_total_disk_space_gb")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? TotalDiskSpaceGb { get; set; }

    /// <summary>
    /// Free disk space in GB
    /// </summary>
    [JsonPropertyName("_free_disk_space_gb")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? FreeDiskSpaceGb { get; set; }

    /// <summary>
    /// Device time zone abbreviation (e.g., "PST")
    /// </summary>
    [JsonPropertyName("_device_time_zone_abbr")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? DeviceTimeZoneAbbr { get; set; }

    /// <summary>
    /// Inferred event name (for automatic events)
    /// </summary>
    [JsonPropertyName("_inferred_event_name")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? InferredEventName { get; set; }

    /// <summary>
    /// Is implicit (0 or 1)
    /// </summary>
    [JsonPropertyName("_implicit")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? IsImplicit { get; set; }

    /// <summary>
    /// Log time (Unix timestamp in seconds)
    /// </summary>
    [JsonPropertyName("_logTime")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public long? LogTime { get; set; }

    /// <summary>
    /// Additional event properties
    /// </summary>
    [JsonExtensionData]
    public Dictionary<string, object>? AdditionalProperties { get; set; }
}
