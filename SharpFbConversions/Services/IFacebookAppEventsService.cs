namespace SharpFbConversions.Services;

using SharpFbConversions.Models;

/// <summary>
/// Service interface for interacting with Facebook App Events Conversions API
/// </summary>
public interface IFacebookAppEventsService
{
    /// <summary>
    /// Send a single app event to Facebook
    /// </summary>
    /// <param name="appEvent">The app event to send</param>
    /// <param name="testEventCode">Optional test event code for testing without affecting live data</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Response from Facebook API</returns>
    Task<AppEventResponse> SendEventAsync(
        AppEvent appEvent, 
        string? testEventCode = null, 
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Send multiple app events to Facebook in a batch
    /// </summary>
    /// <param name="appEvents">Collection of app events to send</param>
    /// <param name="testEventCode">Optional test event code for testing without affecting live data</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Response from Facebook API</returns>
    Task<AppEventResponse> SendEventsAsync(
        IEnumerable<AppEvent> appEvents, 
        string? testEventCode = null, 
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Send app events with full request options
    /// </summary>
    /// <param name="request">Complete app event request with all options</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Response from Facebook API</returns>
    Task<AppEventResponse> SendEventsAsync(
        AppEventRequest request, 
        CancellationToken cancellationToken = default);
}
