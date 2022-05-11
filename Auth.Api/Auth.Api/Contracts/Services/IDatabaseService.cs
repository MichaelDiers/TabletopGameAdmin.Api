namespace Auth.Api.Contracts.Services
{
    using System.Threading.Tasks;
    using Auth.Api.Contracts.Models;

    /// <summary>
    ///     Describes operations on the user database.
    /// </summary>
    public interface IDatabaseService
    {
        /// <summary>
        ///     Create a new user in the database.
        /// </summary>
        /// <param name="user">The user data to be created.</param>
        /// <returns>
        ///     A <see cref="Task{T}" /> whose result is <see cref="ServiceResult.Created" /> if the new user is created and
        ///     <see cref="ServiceResult.AlreadyExists" /> otherwise.
        /// </returns>
        Task<ServiceResult> CreateUser(IUser user);

        /// <summary>
        ///     Read a user from the database.
        /// </summary>
        /// <param name="userName">The name of the user.</param>
        /// <returns>A <see cref="Task{T}" /> whose result is a <see cref="IUser" /> if the user exists and null otherwise.</returns>
        Task<IUser?> ReadAsync(string userName);

        /// <summary>
        ///     Checks if a user with the given name already exists.
        /// </summary>
        /// <param name="userName">The name of the user.</param>
        /// <returns>A <see cref="Task{T}" /> whose result is true if a user exists and false otherwise.</returns>
        Task<bool> UserExists(string userName);
    }
}
