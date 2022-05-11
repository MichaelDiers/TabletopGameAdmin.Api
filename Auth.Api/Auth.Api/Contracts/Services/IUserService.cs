namespace Auth.Api.Contracts.Services
{
    using System.Threading.Tasks;
    using Auth.Api.Contracts.Requests;
    using Auth.Api.Responses;

    /// <summary>
    ///     Business logic of the user controller.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        ///     Authenticate a user and create a json web token.
        /// </summary>
        /// <param name="request">The request that contains the user data.</param>
        /// <returns>
        ///     A <see cref="TokenResponse" /> whose <see cref="TokenResponse.Token" /> if set if the user is authenticated
        ///     and null otherwise.
        /// </returns>
        Task<TokenResponse> SignInAsync(ISignInRequest request);
    }
}
