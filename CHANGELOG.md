# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [1.0.0] - 2025-10-22

### Added
- Initial release of SharpFbConversions
- Facebook App Events Conversions API client service
- Comprehensive model classes:
  - `AppEvent` - Full app event model with all Facebook parameters
  - `UserData` - User data for matching and attribution with hashing support
  - `CustomData` - Custom event parameters
  - `AppData` - App-specific event data
  - `AppEventRequest` - Request payload for batch events
  - `AppEventResponse` - Response with error handling
  - `FacebookError` - Detailed error information
  - `StandardAppEvents` - Constants for all standard Facebook events
  - `FacebookAppEventsOptions` - Configuration options
- Service implementation:
  - `IFacebookAppEventsService` interface
  - `FacebookAppEventsService` implementation with full error handling
- Utilities:
  - `HashUtility` - SHA256 hashing utilities for user data per Facebook requirements
- Extensions:
  - `ServiceCollectionExtensions` - Dependency injection support
- Documentation:
  - Comprehensive README with usage examples
  - EXTENSIBILITY guide for adding future endpoints
  - Example console application
- .NET 8 target framework support
- Full XML documentation for IntelliSense
- Security:
  - No vulnerabilities detected by CodeQL
  - All dependencies verified via GitHub Advisory Database

### Security
- User data hashing utilities following Facebook's security requirements
- Secure handling of access tokens
- No hardcoded secrets or credentials

[1.0.0]: https://github.com/retznutz/SharpFbConversions/releases/tag/v1.0.0