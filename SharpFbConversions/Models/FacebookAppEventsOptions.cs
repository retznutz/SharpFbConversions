namespace SharpFbConversions.Models;

/// <summary>
/// Configuration options for Facebook App Events Conversions API
/// </summary>
public class FacebookAppEventsOptions
{
    /// <summary>
    /// Facebook App ID
    /// </summary>
    public string AppId { get; set; } = string.Empty;

    /// <summary>
    /// Facebook App Access Token for authentication
    /// </summary>
    public string AccessToken { get; set; } = string.Empty;

    /// <summary>
    /// Base URL for Facebook Graph API (default: https://graph.facebook.com)
    /// </summary>
    public string BaseUrl { get; set; } = "https://graph.facebook.com";

    /// <summary>
    /// API Version (default: v22.0)
    /// </summary>
    public string ApiVersion { get; set; } = "v22.0";

    /// <summary>
    /// Enable test mode (events will not be processed)
    /// </summary>
    public bool TestMode { get; set; } = false;
}
