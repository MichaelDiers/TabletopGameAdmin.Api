namespace Auth.Api.Responses
{
    using System.Text.Json.Serialization;

    /// <summary>
    ///     Describes a token response.
    /// </summary>
    public class TokenResponse
    {
        /// <summary>
        ///     Creates a new instance of the <see cref="TokenResponse" /> class.
        /// </summary>
        public TokenResponse()
            : this(null)
        {
        }

        /// <summary>
        ///     Creates a new instance of the <see cref="TokenResponse" /> class.
        /// </summary>
        /// <param name="token">A json web token or null.</param>
        public TokenResponse(string? token)
        {
            this.Token = token;
        }

        /// <summary>
        ///     Gets the json web token.
        /// </summary>
        [JsonPropertyName("token")]
        public string? Token { get; }
    }
}
