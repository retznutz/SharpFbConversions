# Facebook Conversions API Extensibility

This document describes how to extend the SharpFbConversions library to support additional Facebook Conversions API endpoints beyond App Events.

## Current Implementation

The library currently implements the **Facebook App Events Conversions API** with the following structure:

```
SharpFbConversions/
├── Models/                      # Request/response models
│   ├── AppEvent.cs
│   ├── AppEventRequest.cs
│   ├── AppEventResponse.cs
│   ├── UserData.cs
│   ├── CustomData.cs
│   ├── AppData.cs
│   ├── FacebookError.cs
│   └── FacebookAppEventsOptions.cs
├── Services/                    # API services
│   ├── IFacebookAppEventsService.cs
│   └── FacebookAppEventsService.cs
├── Extensions/                  # Dependency injection extensions
│   └── ServiceCollectionExtensions.cs
└── Utilities/                   # Helper utilities
    └── HashUtility.cs
```

## Adding New Facebook Conversion API Endpoints

The library is designed to be extensible. To add support for other Facebook Conversions API endpoints (e.g., Web Events, Offline Events, etc.), follow this pattern:

### 1. Create New Models

Create specific models for the new endpoint in the `Models` folder:

```csharp
// Models/WebEvent.cs
public class WebEvent
{
    [JsonPropertyName("event_name")]
    public string EventName { get; set; }
    
    [JsonPropertyName("event_time")]
    public long EventTime { get; set; }
    
    // ... other properties
}

// Models/WebEventRequest.cs
public class WebEventRequest
{
    [JsonPropertyName("data")]
    public List<WebEvent> Data { get; set; }
    
    // ... other properties
}

// Models/WebEventResponse.cs
public class WebEventResponse
{
    [JsonPropertyName("events_received")]
    public int EventsReceived { get; set; }
    
    // ... other properties
}
```

### 2. Create Configuration Options

Add configuration for the new endpoint:

```csharp
// Models/FacebookWebEventsOptions.cs
public class FacebookWebEventsOptions
{
    public string PixelId { get; set; }
    public string AccessToken { get; set; }
    public string ApiVersion { get; set; } = "v22.0";
}
```

### 3. Create Service Interface

Define the service interface:

```csharp
// Services/IFacebookWebEventsService.cs
public interface IFacebookWebEventsService
{
    Task<WebEventResponse> SendEventAsync(
        WebEvent webEvent,
        CancellationToken cancellationToken = default);
        
    Task<WebEventResponse> SendEventsAsync(
        IEnumerable<WebEvent> webEvents,
        CancellationToken cancellationToken = default);
}
```

### 4. Implement the Service

Implement the service following the same pattern as `FacebookAppEventsService`:

```csharp
// Services/FacebookWebEventsService.cs
public class FacebookWebEventsService : IFacebookWebEventsService
{
    private readonly HttpClient _httpClient;
    private readonly FacebookWebEventsOptions _options;
    private readonly ILogger<FacebookWebEventsService> _logger;

    public FacebookWebEventsService(
        HttpClient httpClient,
        IOptions<FacebookWebEventsOptions> options,
        ILogger<FacebookWebEventsService> logger)
    {
        _httpClient = httpClient;
        _options = options.Value;
        _logger = logger;
    }

    public async Task<WebEventResponse> SendEventAsync(
        WebEvent webEvent,
        CancellationToken cancellationToken = default)
    {
        // Implementation
    }
}
```

### 5. Add Extension Methods

Create extension methods for dependency injection:

```csharp
// Extensions/ServiceCollectionExtensions.cs (add new method)
public static IServiceCollection AddFacebookWebEvents(
    this IServiceCollection services,
    Action<FacebookWebEventsOptions> configureOptions)
{
    services.Configure(configureOptions);
    services.AddHttpClient<IFacebookWebEventsService, FacebookWebEventsService>();
    return services;
}
```

### 6. Update Documentation

Update the README.md with examples for the new endpoint.

## Common Components

The following components can be reused across different Facebook Conversions API endpoints:

### UserData

The `UserData` model is common across all Conversions API endpoints and should be reused:

```csharp
using SharpFbConversions.Models;

var userData = new UserData
{
    Email = HashUtility.HashEmail("user@example.com"),
    Phone = HashUtility.HashPhone("+12345678900")
};
```

### Hash Utility

The `HashUtility` class provides SHA256 hashing for user data and can be used for any endpoint:

```csharp
using SharpFbConversions.Utilities;

var hashedEmail = HashUtility.HashEmail("user@example.com");
```

### Error Handling

The `FacebookError` model is generic and can be used for error responses from any endpoint:

```csharp
using SharpFbConversions.Models;

if (response.Error != null)
{
    Console.WriteLine($"Error: {response.Error.Message}");
}
```

## Planned Future Endpoints

Potential future endpoints to implement:

1. **Web Events (Pixel)** - For sending events from web applications
   - Endpoint: `/{api-version}/{pixel-id}/events`
   - Use case: E-commerce websites, web applications

2. **Offline Events** - For importing offline conversion data
   - Endpoint: `/{api-version}/{dataset-id}/events`
   - Use case: In-store purchases, phone orders, CRM data

3. **Server Events** - For server-side event tracking
   - Endpoint: `/{api-version}/{dataset-id}/events`
   - Use case: Backend systems, server-side conversions

4. **Measurement API** - For attribution and measurement
   - Various endpoints for analytics and reporting

## Best Practices

When extending the library:

1. **Follow Existing Patterns**: Maintain consistency with the current implementation
2. **Add Comprehensive Documentation**: Include XML comments and README examples
3. **Error Handling**: Implement robust error handling like `FacebookAppEventsService`
4. **Logging**: Use structured logging with ILogger
5. **Testing**: Create unit tests for new services
6. **Validation**: Validate required fields and provide helpful error messages
7. **Async/Await**: Use async patterns consistently
8. **Dependency Injection**: Support DI with extension methods
9. **Security**: Hash PII data appropriately using HashUtility

## Contributing

Contributions to add new endpoints are welcome! Please:

1. Follow the patterns established in this library
2. Include comprehensive tests
3. Update documentation
4. Ensure CodeQL security checks pass
5. Submit a pull request with a clear description

## Resources

- [Facebook Conversions API Documentation](https://developers.facebook.com/docs/marketing-api/conversions-api)
- [Facebook App Events API](https://developers.facebook.com/docs/marketing-api/conversions-api/app-events)
- [Facebook Web Events API](https://developers.facebook.com/docs/marketing-api/conversions-api/using-the-api)
- [Facebook Data Parameters](https://developers.facebook.com/docs/marketing-api/conversions-api/parameters)