namespace Auth.Api.Contracts.Responses
{
    /// <summary>
    ///     Describes a token response.
    /// </summary>
    public interface ITokenResponse
    {
        /// <summary>
        ///     Gets the json web token.
        /// </summary>
        string? Token { get; }
    }
}
