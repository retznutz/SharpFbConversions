using System.Text.Json.Serialization;

namespace SharpFbConversions.Models;

/// <summary>
/// Custom data associated with an app event
/// </summary>
public class CustomData
{
    /// <summary>
    /// Value of the event. Use this to track monetary value associated with the event.
    /// </summary>
    [JsonPropertyName("value")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public decimal? ValueToSum { get; set; }

    /// <summary>
    /// Currency for the value, in ISO 4217 format (e.g., "USD", "EUR")
    /// </summary>
    [JsonPropertyName("currency")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Currency { get; set; }

    /// <summary>
    /// Content name (e.g., product name)
    /// </summary>
    [JsonPropertyName("content_name")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ContentName { get; set; }

    /// <summary>
    /// Content category
    /// </summary>
    [JsonPropertyName("content_category")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ContentCategory { get; set; }

    /// <summary>
    /// Content ID (e.g., product ID)
    /// </summary>
    [JsonPropertyName("content_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ContentId { get; set; }

    /// <summary>
    /// Content type (e.g., "product", "product_group")
    /// </summary>
    [JsonPropertyName("content_type")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ContentType { get; set; }

    /// <summary>
    /// Order ID
    /// </summary>
    [JsonPropertyName("order_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? OrderId { get; set; }

    /// <summary>
    /// Predicted lifetime value
    /// </summary>
    [JsonPropertyName("predicted_ltv")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public decimal? PredictedLifetimeValue { get; set; }

    /// <summary>
    /// Number of items
    /// </summary>
    [JsonPropertyName("num_items")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? NumItems { get; set; }

    /// <summary>
    /// Search string
    /// </summary>
    [JsonPropertyName("search_string")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? SearchString { get; set; }

    /// <summary>
    /// Description
    /// </summary>
    [JsonPropertyName("description")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Description { get; set; }

    /// <summary>
    /// Level
    /// </summary>
    [JsonPropertyName("level")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Level { get; set; }

    /// <summary>
    /// Max rating value
    /// </summary>
    [JsonPropertyName("max_rating_value")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? MaxRatingValue { get; set; }

    /// <summary>
    /// Payment info available (0 or 1)
    /// </summary>
    [JsonPropertyName("payment_info_available")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? PaymentInfoAvailable { get; set; }

    /// <summary>
    /// Registration method
    /// </summary>
    [JsonPropertyName("registration_method")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? RegistrationMethod { get; set; }

    /// <summary>
    /// Success (0 or 1)
    /// </summary>
    [JsonPropertyName("success")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? Success { get; set; }

    /// <summary>
    /// Additional custom properties (for any other custom parameters)
    /// </summary>
    [JsonExtensionData]
    public Dictionary<string, object>? AdditionalProperties { get; set; }
}
