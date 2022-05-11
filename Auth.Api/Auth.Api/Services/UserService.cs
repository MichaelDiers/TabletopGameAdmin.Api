namespace Auth.Api.Services
{
    using System.Threading.Tasks;
    using Auth.Api.Contracts.Requests;
    using Auth.Api.Contracts.Services;
    using Auth.Api.Models;
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

        /// <summary>
        ///     Creates a new instance of <see cref="UserService" /> class.
        /// </summary>
        /// <param name="databaseService">Service for accessing the user database.</param>
        /// <param name="jwtService">Service for creating and verifying json web tokens.</param>
        public UserService(IDatabaseService databaseService, IJwtService jwtService)
        {
            this.databaseService = databaseService;
            this.jwtService = jwtService;
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
            var user = await this.databaseService.ReadAsync(request.UserName.ToUpperInvariant());
            if (user == null)
            {
                return new TokenResponse();
            }

            var token = await this.jwtService.CreateAsync(new Payload(user));
            return new TokenResponse(token);
        }
    }
}
