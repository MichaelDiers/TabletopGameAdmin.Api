namespace Auth.Api.Contracts.Models
{
    /// <summary>
    ///     Describes the application settings for creating and verifying json web tokens.
    /// </summary>
    public interface IAppSettingsJwt
    {
        /// <summary>
        ///     Gets the audience for the token.
        /// </summary>
        string Audience { get; }

        /// <summary>
        ///     Gets the expiration time in minutes.
        /// </summary>
        int ExpiresInMinutes { get; }

        /// <summary>
        ///     Gets the issuer of the token.
        /// </summary>
        string Issuer { get; }
    }
}
