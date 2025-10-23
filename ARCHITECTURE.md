# Architecture Overview

## Project Structure

```
SharpFbConversions/
├── SharpFbConversions/              # Main library project (.NET 8)
│   ├── Models/                      # Data models
│   │   ├── AppEvent.cs             # Main event model
│   │   ├── AppEventRequest.cs      # Batch request model
│   │   ├── AppEventResponse.cs     # Response model
│   │   ├── UserData.cs             # User matching data
│   │   ├── CustomData.cs           # Event-specific data
│   │   ├── AppData.cs              # App-specific data
│   │   ├── FacebookError.cs        # Error details
│   │   ├── StandardAppEvents.cs    # Event name constants
│   │   └── FacebookAppEventsOptions.cs # Configuration
│   ├── Services/                    # Service layer
│   │   ├── IFacebookAppEventsService.cs    # Service interface
│   │   └── FacebookAppEventsService.cs     # Implementation
│   ├── Extensions/                  # Extension methods
│   │   └── ServiceCollectionExtensions.cs  # DI registration
│   └── Utilities/                   # Utility classes
│       └── HashUtility.cs          # SHA256 hashing
├── SharpFbConversions.Example/      # Example console app
│   └── Program.cs                   # Usage examples
├── README.md                        # Main documentation
├── EXTENSIBILITY.md                 # Extension guide
├── CHANGELOG.md                     # Version history
├── LICENSE                          # MIT License
└── SharpFbConversions.sln          # Solution file
```

## Design Principles

### 1. Separation of Concerns

- **Models**: Pure data classes with JSON serialization attributes
- **Services**: Business logic for API communication
- **Utilities**: Reusable helper functions
- **Extensions**: Configuration and DI support

### 2. Dependency Injection

The library follows ASP.NET Core DI patterns:

```csharp
services.AddFacebookAppEvents(options =>
{
    options.AppId = "YOUR_APP_ID";
    options.AccessToken = "YOUR_ACCESS_TOKEN";
});
```

### 3. Async/Await

All API calls use async patterns for better performance:

```csharp
var response = await _fbService.SendEventAsync(appEvent);
```

### 4. Strongly-Typed Models

All request/response objects are strongly typed with comprehensive documentation:

```csharp
public class AppEvent
{
    [JsonPropertyName("event_name")]
    public string EventName { get; set; }
    
    // ... with XML documentation
}
```

## Key Components

### FacebookAppEventsService

The main service class handles:
- HTTP communication with Facebook API
- Request serialization
- Response deserialization
- Error handling
- Logging

**Flow:**
1. Client creates `AppEvent` with event data
2. Service validates and serializes to JSON
3. HTTP POST to Facebook API with access token
4. Deserialize and return `AppEventResponse`
5. Log success/errors

### UserData Model

Supports Facebook's user matching parameters:
- Email, phone, name (hashed with SHA256)
- Geographic data (city, state, zip, country)
- Device identifiers (IDFA, GAID)
- Facebook identifiers (FBC, FBP)

### HashUtility

Provides Facebook-compliant SHA256 hashing:
- Normalizes data (lowercase, trim, etc.)
- Computes SHA256 hash
- Returns lowercase hex string

### Authentication

Uses Facebook's access token authentication:
- App-level access token
- Passed as query parameter
- Configured via `FacebookAppEventsOptions`

## API Endpoint

```
POST https://graph.facebook.com/{api-version}/{app-id}/activities?access_token={token}
```

**Request:**
```json
{
  "data": [
    {
      "event_name": "fb_mobile_purchase",
      "event_time": 1234567890,
      "action_source": "app",
      "user_data": { ... },
      "custom_data": { ... }
    }
  ]
}
```

**Response:**
```json
{
  "events_received": 1,
  "fbtrace_id": "..."
}
```

## Error Handling

Three levels of error handling:

1. **HTTP Errors**: Network issues, timeouts
2. **Facebook API Errors**: Invalid parameters, auth failures
3. **Validation Errors**: Missing required fields

All errors return `AppEventResponse` with `FacebookError` populated.

## Extensibility

The architecture supports adding new Facebook Conversions API endpoints:

1. Create new models in `Models/`
2. Create service interface/implementation in `Services/`
3. Add DI extension method in `Extensions/`
4. Reuse common components (`UserData`, `HashUtility`, etc.)

See [EXTENSIBILITY.md](EXTENSIBILITY.md) for details.

## Dependencies

| Package | Purpose | Version |
|---------|---------|---------|
| Microsoft.Extensions.Http | HTTP client factory | 9.0.10 |
| Microsoft.Extensions.Options | Configuration binding | 9.0.10 |
| System.Text.Json | JSON serialization | 9.0.10 |
| Microsoft.Extensions.Configuration.Abstractions | Configuration support | 9.0.10 |

All dependencies are:
- ✅ Secure (verified via GitHub Advisory Database)
- ✅ Actively maintained
- ✅ Compatible with .NET 8

## Testing Strategy

The library is designed for testability:

- Services use dependency injection
- Interfaces enable mocking
- HttpClient uses `IHttpClientFactory`
- No static dependencies

Example test setup:
```csharp
var mockHttp = new MockHttpMessageHandler();
var client = mockHttp.ToHttpClient();
var options = Options.Create(new FacebookAppEventsOptions { ... });
var logger = Mock.Of<ILogger<FacebookAppEventsService>>();
var service = new FacebookAppEventsService(client, options, logger);
```

## Security Considerations

1. **PII Protection**: User data is hashed with SHA256
2. **Token Security**: Access tokens never logged or exposed
3. **HTTPS Only**: All API calls use HTTPS
4. **Input Validation**: Required fields validated
5. **No Secrets**: No hardcoded credentials

## Performance

- Async operations for non-blocking I/O
- HttpClient reuse via `IHttpClientFactory`
- Batch event support (reduce API calls)
- Efficient JSON serialization
- Minimal allocations

## Future Enhancements

- Support for Web Events API
- Support for Offline Events API
- Retry policies with Polly
- Circuit breaker pattern
- Request/response interceptors
- Custom serialization options
- Telemetry and metrics
- NuGet package distribution