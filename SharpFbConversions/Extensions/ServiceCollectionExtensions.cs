using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharpFbConversions.Models;
using SharpFbConversions.Services;

namespace SharpFbConversions.Extensions;

/// <summary>
/// Extension methods for registering Facebook App Events services with dependency injection
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds Facebook App Events services to the service collection
    /// </summary>
    /// <param name="services">The service collection</param>
    /// <param name="configuration">Configuration section containing Facebook options</param>
    /// <returns>The service collection for chaining</returns>
    public static IServiceCollection AddFacebookAppEvents(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<FacebookAppEventsOptions>(configuration);
        services.AddHttpClient<IFacebookAppEventsService, FacebookAppEventsService>();
        return services;
    }

    /// <summary>
    /// Adds Facebook App Events services to the service collection with action configuration
    /// </summary>
    /// <param name="services">The service collection</param>
    /// <param name="configureOptions">Action to configure Facebook options</param>
    /// <returns>The service collection for chaining</returns>
    public static IServiceCollection AddFacebookAppEvents(
        this IServiceCollection services,
        Action<FacebookAppEventsOptions> configureOptions)
    {
        services.Configure(configureOptions);
        services.AddHttpClient<IFacebookAppEventsService, FacebookAppEventsService>();
        return services;
    }
}
