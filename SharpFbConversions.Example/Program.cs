using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SharpFbConversions.Extensions;
using SharpFbConversions.Models;
using SharpFbConversions.Services;
using SharpFbConversions.Utilities;

// Create host builder
var builder = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        // Register Facebook App Events service
        services.AddFacebookAppEvents(options =>
        {
            // In production, load these from configuration or environment variables
            options.AppId = "YOUR_APP_ID";
            options.AccessToken = "YOUR_ACCESS_TOKEN";
            options.ApiVersion = "v22.0";
            options.TestMode = true; // Set to false in production
        });
    });

var host = builder.Build();

// Get the service and logger
var fbService = host.Services.GetRequiredService<IFacebookAppEventsService>();
var logger = host.Services.GetRequiredService<ILogger<Program>>();

logger.LogInformation("Starting Facebook App Events example...");

try
{
    // Example 1: Send a purchase event
    await SendPurchaseEventExample(fbService, logger);

    // Example 2: Send multiple events in batch
    await SendBatchEventsExample(fbService, logger);

    // Example 3: Send custom event with full user data
    await SendCustomEventExample(fbService, logger);
}
catch (Exception ex)
{
    logger.LogError(ex, "An error occurred while sending events");
}

logger.LogInformation("Example completed.");

static async Task SendPurchaseEventExample(IFacebookAppEventsService fbService, ILogger logger)
{
    logger.LogInformation("=== Example 1: Purchase Event ===");

    var purchaseEvent = new AppEvent
    {
        EventName = StandardAppEvents.Purchase,
        EventTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
        EventId = Guid.NewGuid().ToString(), // For deduplication
        ActionSource = "app",
        UserData = new UserData
        {
            Email = HashUtility.HashEmail("user@example.com"),
            Phone = HashUtility.HashPhone("+12345678900"),
            ExternalId = "user_12345"
        },
        CustomData = new CustomData
        {
            ValueToSum = 99.99m,
            Currency = "USD",
            ContentName = "Premium Subscription",
            ContentId = "sub_premium_001",
            ContentType = "product"
        }
    };

    var response = await fbService.SendEventAsync(purchaseEvent, testEventCode: "TEST12345");

    if (response.Error != null)
    {
        logger.LogError("Error sending purchase event: {Message}", response.Error.Message);
    }
    else
    {
        logger.LogInformation("Purchase event sent successfully. Events received: {Count}", response.EventsReceived);
    }
}

static async Task SendBatchEventsExample(IFacebookAppEventsService fbService, ILogger logger)
{
    logger.LogInformation("=== Example 2: Batch Events ===");

    var events = new List<AppEvent>
    {
        new AppEvent
        {
            EventName = StandardAppEvents.ContentView,
            EventTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
            ActionSource = "app",
            CustomData = new CustomData
            {
                ContentId = "product_123",
                ContentName = "Amazing Product"
            }
        },
        new AppEvent
        {
            EventName = StandardAppEvents.AddToCart,
            EventTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
            ActionSource = "app",
            CustomData = new CustomData
            {
                ContentId = "product_123",
                ValueToSum = 49.99m,
                Currency = "USD"
            }
        },
        new AppEvent
        {
            EventName = StandardAppEvents.InitiatedCheckout,
            EventTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
            ActionSource = "app",
            CustomData = new CustomData
            {
                ValueToSum = 49.99m,
                Currency = "USD",
                NumItems = 1
            }
        }
    };

    var response = await fbService.SendEventsAsync(events, testEventCode: "TEST12345");

    if (response.Error != null)
    {
        logger.LogError("Error sending batch events: {Message}", response.Error.Message);
    }
    else
    {
        logger.LogInformation("Batch events sent successfully. Events received: {Count}", response.EventsReceived);
    }
}

static async Task SendCustomEventExample(IFacebookAppEventsService fbService, ILogger logger)
{
    logger.LogInformation("=== Example 3: Custom Event with Full User Data ===");

    var customEvent = new AppEvent
    {
        EventName = "CustomGameAchievement",
        EventTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
        EventId = Guid.NewGuid().ToString(),
        ActionSource = "app",
        AppVersion = "2.1.0",
        OsVersion = "iOS 15.0",
        DeviceModel = "iPhone 13",
        Locale = "en_US",
        Timezone = "America/Los_Angeles",
        UserData = new UserData
        {
            Email = HashUtility.HashEmail("player@example.com"),
            FirstName = HashUtility.HashSha256("john"),
            LastName = HashUtility.HashSha256("doe"),
            City = HashUtility.HashCity("Seattle"),
            State = HashUtility.HashState("WA"),
            ZipCode = HashUtility.HashZipCode("98101"),
            Country = HashUtility.HashCountry("us"),
            ExternalId = "player_12345"
        },
        CustomData = new CustomData
        {
            Level = "15",
            Description = "Unlocked Dragon Slayer Achievement",
            Success = 1
        },
        AppData = new AppData
        {
            ApplicationTrackingEnabled = 1,
            AdvertiserTrackingEnabled = 1
        }
    };

    var response = await fbService.SendEventAsync(customEvent, testEventCode: "TEST12345");

    if (response.Error != null)
    {
        logger.LogError("Error sending custom event: {Message}", response.Error.Message);
    }
    else
    {
        logger.LogInformation("Custom event sent successfully. Events received: {Count}", response.EventsReceived);
        if (response.Messages != null && response.Messages.Any())
        {
            logger.LogInformation("Messages: {Messages}", string.Join(", ", response.Messages));
        }
    }
}
