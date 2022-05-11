namespace Auth.Api.Services
{
    using System.Threading.Tasks;
    using Auth.Api.Contracts.Models;
    using Auth.Api.Contracts.Requests;
    using Auth.Api.Contracts.Services;
    using Auth.Api.Models;

    /// <summary>
    ///     Service operations that require admin privileges.
    /// </summary>
    public class AdminService : IAdminService
    {
        /// <summary>
        ///     Access the user database.
        /// </summary>
        private readonly IDatabaseService databaseService;

        /// <summary>
        ///     A service for creating and verifying json web tokens.
        /// </summary>
        private readonly IJwtService jwtService;

        /// <summary>
        ///     Service hashing and verifying passwords.
        /// </summary>
        private readonly IPasswordHashService passwordHashService;

        /// <summary>
        ///     Creates a new instance of the <see cref="AdminService" /> class.
        /// </summary>
        /// <param name="passwordHashService">Service hashing and verifying passwords.</param>
        /// <param name="databaseService">Service for accessing the user database.</param>
        /// <param name="jwtService">A service for creating and verifying json web tokens.</param>
        public AdminService(
            IPasswordHashService passwordHashService,
            IDatabaseService databaseService,
            IJwtService jwtService
        )
        {
            this.passwordHashService = passwordHashService;
            this.databaseService = databaseService;
            this.jwtService = jwtService;
        }

        /// <summary>
        ///     Create a new user.
        /// </summary>
        /// <param name="request">The user data for creating a new user.</param>
        /// <param name="adminToken">A token of an admin privileged user.</param>
        /// <returns>A <see cref="Task" /> whose <see cref="ServiceResult" /> indicates success or failure.</returns>
        public async Task<ServiceResult> CreateUser(ICreateUserRequest request, string adminToken)
        {
            var payload = await this.jwtService.VerifyAsync(adminToken);
            if (payload == null || (payload.Roles & Roles.AuthAdmin) != Roles.AuthAdmin)
            {
                return ServiceResult.MissingPrivileges;
            }

            var hash = this.passwordHashService.Hash(request.Password);
            var user = new User(
                request.UserName.ToUpperInvariant(),
                hash,
                request.UserName,
                request.Email,
                request.Roles);

            if (await this.databaseService.UserExists(user.UserName))
            {
                return ServiceResult.AlreadyExists;
            }

            return await this.databaseService.CreateUser(user);
        }
    }
}
