namespace Auth.Api.Contracts.Services
{
    using System.Threading.Tasks;
    using Auth.Api.Contracts.Requests;

    /// <summary>
    ///     Service operations that require admin privileges.
    /// </summary>
    public interface IAdminService
    {
        /// <summary>
        ///     Create a new user.
        /// </summary>
        /// <param name="request">The user data for creating a new user.</param>
        /// <param name="adminToken">A token of an admin privileged user.</param>
        /// <returns>A <see cref="Task" /> whose <see cref="ServiceResult" /> indicates success or failure.</returns>
        Task<ServiceResult> CreateUser(ICreateUserRequest request, string adminToken);
    }
}
