using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SharpFbConversions.Models;

namespace SharpFbConversions.Services;

/// <summary>
/// Service implementation for Facebook App Events Conversions API
/// </summary>
public class FacebookAppEventsService : IFacebookAppEventsService
{
    private readonly HttpClient _httpClient;
    private readonly FacebookAppEventsOptions _options;
    private readonly ILogger<FacebookAppEventsService> _logger;
    private readonly JsonSerializerOptions _jsonOptions;

    /// <summary>
    /// Initializes a new instance of the FacebookAppEventsService
    /// </summary>
    /// <param name="httpClient">HTTP client for making API calls</param>
    /// <param name="options">Configuration options</param>
    /// <param name="logger">Logger instance</param>
    public FacebookAppEventsService(
        HttpClient httpClient,
        IOptions<FacebookAppEventsOptions> options,
        ILogger<FacebookAppEventsService> logger)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        // Validate required options
        if (string.IsNullOrWhiteSpace(_options.AppId))
            throw new ArgumentException("AppId is required", nameof(options));
        if (string.IsNullOrWhiteSpace(_options.AccessToken))
            throw new ArgumentException("AccessToken is required", nameof(options));

        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
        };

        // Configure base address
        _httpClient.BaseAddress = new Uri(_options.BaseUrl);
    }

    /// <inheritdoc/>
    public async Task<AppEventResponse> SendEventAsync(
        AppEvent appEvent,
        string? testEventCode = null,
        CancellationToken cancellationToken = default)
    {
        if (appEvent == null)
            throw new ArgumentNullException(nameof(appEvent));

        return await SendEventsAsync(new[] { appEvent }, testEventCode, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<AppEventResponse> SendEventsAsync(
        IEnumerable<AppEvent> appEvents,
        string? testEventCode = null,
        CancellationToken cancellationToken = default)
    {
        if (appEvents == null)
            throw new ArgumentNullException(nameof(appEvents));

        var request = new AppEventRequest
        {
            Data = appEvents.ToList(),
            TestEventCode = testEventCode ?? (_options.TestMode ? "TEST_EVENT_CODE" : null)
        };

        return await SendEventsAsync(request, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<AppEventResponse> SendEventsAsync(
        AppEventRequest request,
        CancellationToken cancellationToken = default)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));

        if (request.Data == null || !request.Data.Any())
            throw new ArgumentException("At least one event is required", nameof(request));

        try
        {
            var endpoint = $"/{_options.ApiVersion}/{_options.AppId}/activities";
            var url = $"{endpoint}?access_token={_options.AccessToken}";

            _logger.LogInformation("Sending {Count} app event(s) to Facebook API", request.Data.Count);

            var response = await _httpClient.PostAsJsonAsync(url, request, _jsonOptions, cancellationToken);

            var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                var result = JsonSerializer.Deserialize<AppEventResponse>(responseContent, _jsonOptions);
                
                if (result == null)
                {
                    _logger.LogWarning("Received null response from Facebook API");
                    return new AppEventResponse
                    {
                        EventsReceived = 0,
                        Error = new FacebookError
                        {
                            Message = "Invalid response from Facebook API",
                            Code = -1
                        }
                    };
                }

                _logger.LogInformation(
                    "Successfully sent events. Received: {Received}, Dropped: {Dropped}", 
                    result.EventsReceived, 
                    result.EventsDropped ?? 0);

                return result;
            }
            else
            {
                _logger.LogError(
                    "Facebook API returned error. Status: {StatusCode}, Response: {Response}",
                    response.StatusCode,
                    responseContent);

                // Try to parse error response
                try
                {
                    var errorResponse = JsonSerializer.Deserialize<AppEventResponse>(responseContent, _jsonOptions);
                    if (errorResponse?.Error != null)
                    {
                        return errorResponse;
                    }
                }
                catch (JsonException)
                {
                    // If we can't parse the error, create a generic error response
                }

                return new AppEventResponse
                {
                    EventsReceived = 0,
                    Error = new FacebookError
                    {
                        Message = $"HTTP {(int)response.StatusCode}: {response.ReasonPhrase}",
                        Code = (int)response.StatusCode
                    }
                };
            }
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "HTTP request exception while sending events to Facebook");
            return new AppEventResponse
            {
                EventsReceived = 0,
                Error = new FacebookError
                {
                    Message = $"HTTP request failed: {ex.Message}",
                    Code = -1,
                    IsTransient = true
                }
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error while sending events to Facebook");
            return new AppEventResponse
            {
                EventsReceived = 0,
                Error = new FacebookError
                {
                    Message = $"Unexpected error: {ex.Message}",
                    Code = -1
                }
            };
        }
    }
}
