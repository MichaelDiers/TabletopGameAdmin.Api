namespace Auth.Api.Responses
{
    using Auth.Api.Contracts.Responses;

    /// <summary>
    ///     Describes a token response.
    /// </summary>
    public class TokenResponse : ITokenResponse
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
        public string? Token { get; }
    }
}
