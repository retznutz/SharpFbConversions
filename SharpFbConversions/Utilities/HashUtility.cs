using System.Security.Cryptography;
using System.Text;

namespace SharpFbConversions.Utilities;

/// <summary>
/// Utility methods for hashing user data according to Facebook requirements
/// </summary>
public static class HashUtility
{
    /// <summary>
    /// Hashes a string value using SHA256 as required by Facebook
    /// </summary>
    /// <param name="value">Value to hash</param>
    /// <returns>SHA256 hash in lowercase hexadecimal format</returns>
    public static string? HashSha256(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return null;

        // Normalize: trim and lowercase
        var normalized = value.Trim().ToLowerInvariant();

        using var sha256 = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(normalized);
        var hash = sha256.ComputeHash(bytes);
        
        return Convert.ToHexString(hash).ToLowerInvariant();
    }

    /// <summary>
    /// Hashes an email address according to Facebook requirements
    /// </summary>
    /// <param name="email">Email address to hash</param>
    /// <returns>SHA256 hash of the normalized email</returns>
    public static string? HashEmail(string? email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return null;

        // Remove whitespace and convert to lowercase
        var normalized = email.Trim().ToLowerInvariant();
        return HashSha256(normalized);
    }

    /// <summary>
    /// Hashes a phone number according to Facebook requirements
    /// </summary>
    /// <param name="phone">Phone number to hash (should include country code)</param>
    /// <returns>SHA256 hash of the normalized phone number</returns>
    public static string? HashPhone(string? phone)
    {
        if (string.IsNullOrWhiteSpace(phone))
            return null;

        // Remove all non-digit characters
        var normalized = new string(phone.Where(char.IsDigit).ToArray());
        return HashSha256(normalized);
    }

    /// <summary>
    /// Hashes a gender value according to Facebook requirements
    /// </summary>
    /// <param name="gender">Gender (m, f, or other single character)</param>
    /// <returns>SHA256 hash of the normalized gender</returns>
    public static string? HashGender(string? gender)
    {
        if (string.IsNullOrWhiteSpace(gender))
            return null;

        // Take first character and lowercase
        var normalized = gender.Trim().ToLowerInvariant().Substring(0, 1);
        return HashSha256(normalized);
    }

    /// <summary>
    /// Hashes a date of birth according to Facebook requirements
    /// </summary>
    /// <param name="dateOfBirth">Date of birth</param>
    /// <returns>SHA256 hash of the date in YYYYMMDD format</returns>
    public static string? HashDateOfBirth(DateTime? dateOfBirth)
    {
        if (!dateOfBirth.HasValue)
            return null;

        var normalized = dateOfBirth.Value.ToString("yyyyMMdd");
        return HashSha256(normalized);
    }

    /// <summary>
    /// Hashes a city name according to Facebook requirements
    /// </summary>
    /// <param name="city">City name</param>
    /// <returns>SHA256 hash of the normalized city name</returns>
    public static string? HashCity(string? city)
    {
        if (string.IsNullOrWhiteSpace(city))
            return null;

        // Remove spaces, punctuation, and lowercase
        var normalized = new string(city.Where(c => char.IsLetterOrDigit(c)).ToArray()).ToLowerInvariant();
        return HashSha256(normalized);
    }

    /// <summary>
    /// Hashes a state code according to Facebook requirements
    /// </summary>
    /// <param name="state">State code (2-letter state abbreviation)</param>
    /// <returns>SHA256 hash of the normalized state code</returns>
    public static string? HashState(string? state)
    {
        if (string.IsNullOrWhiteSpace(state))
            return null;

        var normalized = state.Trim().ToLowerInvariant();
        return HashSha256(normalized);
    }

    /// <summary>
    /// Hashes a zip code according to Facebook requirements
    /// </summary>
    /// <param name="zipCode">Zip or postal code</param>
    /// <returns>SHA256 hash of the normalized zip code</returns>
    public static string? HashZipCode(string? zipCode)
    {
        if (string.IsNullOrWhiteSpace(zipCode))
            return null;

        // Remove whitespace and special characters, keep only alphanumeric
        var normalized = new string(zipCode.Where(char.IsLetterOrDigit).ToArray()).ToLowerInvariant();
        return HashSha256(normalized);
    }

    /// <summary>
    /// Hashes a country code according to Facebook requirements
    /// </summary>
    /// <param name="country">ISO 3166-1 alpha-2 country code</param>
    /// <returns>SHA256 hash of the normalized country code</returns>
    public static string? HashCountry(string? country)
    {
        if (string.IsNullOrWhiteSpace(country))
            return null;

        var normalized = country.Trim().ToLowerInvariant();
        return HashSha256(normalized);
    }
}
