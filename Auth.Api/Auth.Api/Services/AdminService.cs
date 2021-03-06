namespace Auth.Api.Services
{
    using System.Threading.Tasks;
    using Auth.Api.Contracts.Requests;
    using Auth.Api.Contracts.Services;
    using Auth.Api.Extensions;
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
        ///     Service hashing and verifying passwords.
        /// </summary>
        private readonly IPasswordHashService passwordHashService;

        /// <summary>
        ///     Creates a new instance of the <see cref="AdminService" /> class.
        /// </summary>
        /// <param name="passwordHashService">Service hashing and verifying passwords.</param>
        /// <param name="databaseService">Service for accessing the user database.</param>
        public AdminService(IPasswordHashService passwordHashService, IDatabaseService databaseService)
        {
            this.passwordHashService = passwordHashService;
            this.databaseService = databaseService;
        }

        /// <summary>
        ///     Create a new user.
        /// </summary>
        /// <param name="request">The user data for creating a new user.</param>
        /// <returns>A <see cref="Task" /> whose <see cref="ServiceResult" /> indicates success or failure.</returns>
        public async Task<ServiceResult> CreateUser(ICreateUserRequest request)
        {
            var hash = this.passwordHashService.Hash(request.Password);
            var user = new User(
                request.UserName.NormalizeUserName(),
                hash,
                request.UserName,
                request.Email,
                request.Roles);

            if (await this.databaseService.UserExistsAsync(user.UserName))
            {
                return ServiceResult.Conflict;
            }

            return await this.databaseService.CreateUserAsync(user);
        }

        /// <summary>
        ///     Delete all generic test users.
        /// </summary>
        /// <returns>A <see cref="ServiceResult.Deleted" /> or <see cref="ServiceResult.NotFound" />.</returns>
        public async Task<ServiceResult> DeleteGenericUsersAsync()
        {
            return await this.databaseService.DeleteGenericUsersAsync();
        }
    }
}
