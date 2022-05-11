namespace Auth.Api.Contracts.Services
{
    using System.Threading.Tasks;
    using Auth.Api.Contracts.Models;

    /// <summary>
    ///     Service for creating json web tokens.
    /// </summary>
    public interface IJwtService
    {
        /// <summary>
        ///     Create new json web token.
        /// </summary>
        /// <param name="payload">The payload of the json web token.</param>
        /// <returns>A new token.</returns>
        Task<string> CreateAsync(IPayload payload);

        /// <summary>
        ///     Verify the given json web token.
        /// </summary>
        /// <param name="token">The json web token that will be verified.</param>
        /// <returns>A <see cref="Task{T}" /> whose result is the payload of the json web token or null if the token is invalid.</returns>
        Task<IPayload?> VerifyAsync(string token);
    }
}
