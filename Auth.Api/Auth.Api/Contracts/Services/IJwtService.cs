namespace Auth.Api.Contracts.Services
{
    using Auth.Api.Contracts.Models;

    /// <summary>
    ///     Service for creating json web tokens.
    /// </summary>
    public interface IJwtService
    {
        /// <summary>
        ///     Create new json web token.
        /// </summary>
        /// <param name="user">The user data for that the token is created.</param>
        /// <returns>A new token.</returns>
        string Create(IUser user);
    }
}
