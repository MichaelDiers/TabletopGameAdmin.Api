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
        ///     Update the password for a user.
        /// </summary>
        /// <param name="request">The change password request.</param>
        /// <returns>A <see cref="Task{T}" /> whose result is a <see cref="ServiceResult" />.</returns>
        Task<ServiceResult> ChangePassword(IChangePasswordRequest request);

        /// <summary>
        ///     Delete a user.
        /// </summary>
        /// <param name="request">The request that contains the user to be deleted.</param>
        /// <returns>A <see cref="Task" /> whose result is a <see cref="ServiceResult" />.</returns>
        Task<ServiceResult> DeleteUser(IDeleteUserRequest request);

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
