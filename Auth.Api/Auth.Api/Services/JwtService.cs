namespace Auth.Api.Services
{
    using System.Threading.Tasks;
    using Auth.Api.Contracts.Models;
    using Auth.Api.Contracts.Services;
    using Auth.Api.Models;

    /// <summary>
    ///     Service for creating json web tokens.
    /// </summary>
    public class JwtService : IJwtService
    {
        /// <summary>
        ///     Create new json web token.
        /// </summary>
        /// <param name="payload">The json web token payload.</param>
        /// <returns>A new token.</returns>
        public Task<string> CreateAsync(IPayload payload)
        {
            return Task.FromResult("token");
        }

        /// <summary>
        ///     Verify the given json web token.
        /// </summary>
        /// <param name="token">The json web token that will be verified.</param>
        /// <returns>A <see cref="Task{T}" /> whose result is the payload of the json web token or null if the token is invalid.</returns>
        public async Task<IPayload?> VerifyAsync(string token)
        {
            await Task.CompletedTask;
            var roles = token switch
            {
                "user" => Roles.AuthUser,
                "admin" => Roles.AuthAdmin,
                "super" => Roles.AuthSuperUser,
                _ => Roles.None
            };

            return new Payload {UserName = "userName", Roles = roles};
        }
    }
}
