# SharpFbConversions

A comprehensive .NET 8 client library for Facebook App Events Conversions API. This library provides a strongly-typed, easy-to-use interface for sending app events to Facebook's Marketing API.

## Features

- ✅ Full support for Facebook App Events Conversions API
- ✅ Strongly-typed models for all request/response objects
- ✅ Built-in authentication support
- ✅ Comprehensive user data hashing utilities (SHA256)
- ✅ Standard Facebook app event constants
- ✅ Dependency injection support
- ✅ Async/await pattern
- ✅ Detailed XML documentation
- ✅ .NET 8 target framework

## Installation

```bash
dotnet add package SharpFbConversions
```

## Quick Start

### 1. Configuration

Configure the service in your `appsettings.json`:

```json
{
  "FacebookAppEvents": {
    "AppId": "your-app-id",
    "AccessToken": "your-access-token",
    "ApiVersion": "v22.0",
    "TestMode": false
  }
}
```

### 2. Register Services

In your `Program.cs` or `Startup.cs`:

```csharp
using SharpFbConversions.Extensions;

// Using configuration
builder.Services.AddFacebookAppEvents(
    builder.Configuration.GetSection("FacebookAppEvents"));

// Or using action configuration
builder.Services.AddFacebookAppEvents(options =>
{
    options.AppId = "your-app-id";
    options.AccessToken = "your-access-token";
    options.ApiVersion = "v22.0";
    options.TestMode = false;
});
```

### 3. Send Events

```csharp
using SharpFbConversions.Models;
using SharpFbConversions.Services;
using SharpFbConversions.Utilities;

public class MyService
{
    private readonly IFacebookAppEventsService _fbService;

    public MyService(IFacebookAppEventsService fbService)
    {
        _fbService = fbService;
    }

    public async Task SendPurchaseEvent()
    {
        var appEvent = new AppEvent
        {
            EventName = StandardAppEvents.Purchase,
            EventTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
            ActionSource = "app",
            UserData = new UserData
            {
                Email = HashUtility.HashEmail("user@example.com"),
                Phone = HashUtility.HashPhone("+12345678900"),
                ExternalId = "user123"
            },
            CustomData = new CustomData
            {
                ValueToSum = 99.99m,
                Currency = "USD",
                ContentName = "Premium Subscription",
                ContentId = "sub_premium_001"
            }
        };

        var response = await _fbService.SendEventAsync(appEvent);
        
        if (response.Error != null)
        {
            Console.WriteLine($"Error: {response.Error.Message}");
        }
        else
        {
            Console.WriteLine($"Events received: {response.EventsReceived}");
        }
    }
}
```

## Standard App Events

The library includes constants for all standard Facebook app events:

```csharp
using SharpFbConversions.Models;

// Standard events
StandardAppEvents.Purchase
StandardAppEvents.AddToCart
StandardAppEvents.InitiatedCheckout
StandardAppEvents.CompleteRegistration
StandardAppEvents.ActivateApp
StandardAppEvents.ContentView
StandardAppEvents.Search
StandardAppEvents.AddToWishlist
StandardAppEvents.AddPaymentInfo
StandardAppEvents.LevelAchieved
StandardAppEvents.AchievementUnlocked
StandardAppEvents.TutorialCompletion
// ... and more
```

## User Data Hashing

Facebook requires certain user data fields to be hashed with SHA256. The library provides utility methods:

```csharp
using SharpFbConversions.Utilities;

var hashedEmail = HashUtility.HashEmail("user@example.com");
var hashedPhone = HashUtility.HashPhone("+1-234-567-8900");
var hashedGender = HashUtility.HashGender("f");
var hashedDob = HashUtility.HashDateOfBirth(new DateTime(1990, 1, 15));
var hashedCity = HashUtility.HashCity("New York");
var hashedState = HashUtility.HashState("NY");
var hashedZip = HashUtility.HashZipCode("10001");
var hashedCountry = HashUtility.HashCountry("us");
```

## Batch Events

Send multiple events in a single request:

```csharp
var events = new List<AppEvent>
{
    new AppEvent
    {
        EventName = StandardAppEvents.ContentView,
        EventTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
        ActionSource = "app"
    },
    new AppEvent
    {
        EventName = StandardAppEvents.AddToCart,
        EventTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
        ActionSource = "app"
    }
};

var response = await _fbService.SendEventsAsync(events);
```

## Test Events

For testing without affecting live data:

```csharp
var response = await _fbService.SendEventAsync(
    appEvent, 
    testEventCode: "TEST12345");
```

## Advanced Usage

### Custom Event with All Parameters

```csharp
var appEvent = new AppEvent
{
    EventName = "CustomPurchase",
    EventTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
    EventId = Guid.NewGuid().ToString(), // For deduplication
    ActionSource = "app",
    AppVersion = "2.1.0",
    OsVersion = "iOS 15.0",
    DeviceModel = "iPhone 13",
    Locale = "en_US",
    Timezone = "America/Los_Angeles",
    UserData = new UserData
    {
        Email = HashUtility.HashEmail("user@example.com"),
        Phone = HashUtility.HashPhone("+12345678900"),
        FirstName = HashUtility.HashSha256("john"),
        LastName = HashUtility.HashSha256("doe"),
        City = HashUtility.HashCity("Seattle"),
        State = HashUtility.HashState("WA"),
        ZipCode = HashUtility.HashZipCode("98101"),
        Country = HashUtility.HashCountry("us"),
        ExternalId = "user123",
        ClientIpAddress = "192.168.1.1",
        ClientUserAgent = "Mozilla/5.0..."
    },
    CustomData = new CustomData
    {
        ValueToSum = 149.99m,
        Currency = "USD",
        ContentName = "Premium Product",
        ContentCategory = "Subscription",
        ContentId = "prod_123",
        ContentType = "product",
        OrderId = "order_456",
        NumItems = 1
    },
    AppData = new AppData
    {
        ApplicationTrackingEnabled = 1,
        AdvertiserTrackingEnabled = 1
    }
};

var response = await _fbService.SendEventAsync(appEvent);
```

### Full Request Options

```csharp
var request = new AppEventRequest
{
    Data = new List<AppEvent> { appEvent },
    TestEventCode = "TEST12345",
    PartnerAgent = "MyApp/1.0",
    UploadTag = "batch_001"
};

var response = await _fbService.SendEventsAsync(request);
```

## API Reference

### Models

- **FacebookAppEventsOptions**: Configuration options for the service
- **AppEvent**: Represents a single app event
- **UserData**: User information for matching and attribution
- **CustomData**: Custom event parameters
- **AppData**: App-specific event data
- **AppEventRequest**: Request payload for sending events
- **AppEventResponse**: Response from Facebook API
- **FacebookError**: Error details from Facebook API
- **StandardAppEvents**: Constants for standard Facebook events

### Services

- **IFacebookAppEventsService**: Service interface
  - `SendEventAsync()`: Send a single event
  - `SendEventsAsync()`: Send multiple events
  - `SendEventsAsync(AppEventRequest)`: Send with full request options

### Utilities

- **HashUtility**: SHA256 hashing utilities for user data
  - `HashEmail()`, `HashPhone()`, `HashGender()`, `HashDateOfBirth()`
  - `HashCity()`, `HashState()`, `HashZipCode()`, `HashCountry()`
  - `HashSha256()`: Generic string hashing

## Documentation

For complete API documentation, see [Facebook App Events API Documentation](https://developers.facebook.com/docs/marketing-api/conversions-api/app-events).

## Requirements

- .NET 8.0 or later
- Facebook App ID and Access Token

## License

This project is licensed under the MIT License.

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## Support

For issues and questions, please use the GitHub issue tracker.