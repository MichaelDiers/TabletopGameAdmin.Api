namespace Auth.Api.Models
{
    using Auth.Api.Contracts.Models;

    /// <summary>
    ///     Describes the application settings for the json web token service.
    /// </summary>
    public class AppSettingsJwt : IAppSettingsJwt
    {
        /// <summary>
        ///     Gets or sets the audience of tokens.
        /// </summary>
        public string Audience { get; set; } = string.Empty;

        /// <summary>
        ///     Gets or sets the expiration time in minutes of new tokens.
        /// </summary>
        public int ExpiresInMinutes { get; set; }

        /// <summary>
        ///     Gets or sets the issuer of tokens.
        /// </summary>
        public string Issuer { get; set; } = string.Empty;
    }
}
