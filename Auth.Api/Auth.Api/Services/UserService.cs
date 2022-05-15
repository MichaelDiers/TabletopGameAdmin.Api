namespace Auth.Api.Services
{
    using System.Threading.Tasks;
    using Auth.Api.Contracts.Requests;
    using Auth.Api.Contracts.Services;
    using Auth.Api.Extensions;
    using Auth.Api.Responses;

    /// <summary>
    ///     Business logic of the user controller.
    /// </summary>
    public class UserService : IUserService
    {
        /// <summary>
        ///     Service for accessing the user database.
        /// </summary>
        private readonly IDatabaseService databaseService;

        /// <summary>
        ///     Service for creating and verifying json web tokens.
        /// </summary>
        private readonly IJwtService jwtService;

        private readonly IPasswordHashService passwordHashService;

        /// <summary>
        ///     Creates a new instance of <see cref="UserService" /> class.
        /// </summary>
        /// <param name="databaseService">Service for accessing the user database.</param>
        /// <param name="jwtService">Service for creating and verifying json web tokens.</param>
        /// <param name="passwordHashService">Service for hashing and verifying passwords.</param>
        public UserService(
            IDatabaseService databaseService,
            IJwtService jwtService,
            IPasswordHashService passwordHashService
        )
        {
            this.databaseService = databaseService;
            this.jwtService = jwtService;
            this.passwordHashService = passwordHashService;
        }

        /// <summary>
        ///     Update the password for a user.
        /// </summary>
        /// <param name="request">The change password request.</param>
        /// <returns>A <see cref="Task{T}" /> whose result is a <see cref="ServiceResult" />.</returns>
        public async Task<ServiceResult> ChangePassword(IChangePasswordRequest request)
        {
            var signInResult = await this.SignInAsync(request);
            if (string.IsNullOrWhiteSpace(signInResult.Token))
            {
                return ServiceResult.NotFound;
            }

            var newPassword = this.passwordHashService.Hash(request.NewPassword);
            await this.databaseService.ChangePassword(request.UserName.NormalizeUserName(), newPassword);
            return ServiceResult.Updated;
        }

        /// <summary>
        ///     Delete a user.
        /// </summary>
        /// <param name="request">The request that contains the user to be deleted.</param>
        /// <returns>A <see cref="Task" /> whose result is an <see cref="ServiceResult" />.</returns>
        public async Task<ServiceResult> DeleteUser(IDeleteUserRequest request)
        {
            return await this.databaseService.DeleteUserAsync(request.UserName.NormalizeUserName());
        }

        /// <summary>
        ///     Authenticate a user and create a json web token.
        /// </summary>
        /// <param name="request">The request that contains the user data.</param>
        /// <returns>
        ///     A <see cref="TokenResponse" /> whose <see cref="TokenResponse.Token" /> if set if the user is authenticated
        ///     and null otherwise.
        /// </returns>
        public async Task<TokenResponse> SignInAsync(ISignInRequest request)
        {
            var user = await this.databaseService.ReadAsync(request.UserName.NormalizeUserName());
            if (user == null || !this.passwordHashService.VerifyHash(request.Password, user.Password))
            {
                return new TokenResponse();
            }

            var token = this.jwtService.Create(user);
            return new TokenResponse(token);
        }
    }
}
