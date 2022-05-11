namespace Auth.Api.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Auth.Api.Contracts.Models;
    using Auth.Api.Contracts.Services;

    /// <summary>
    ///     Describes operations on the user database.
    /// </summary>
    public class DatabaseService : IDatabaseService
    {
        /// <summary>
        ///     In memory database.
        /// </summary>
        private readonly IList<IUser> users = new List<IUser>();

        /// <summary>
        ///     Create a new user.
        /// </summary>
        /// <param name="user">The user data to be created.</param>
        /// <returns>
        ///     A <see cref="ServiceResult.Created" /> if the new user is created and
        ///     <see cref="ServiceResult.AlreadyExists" /> otherwise.
        /// </returns>
        public async Task<ServiceResult> CreateUser(IUser user)
        {
            await Task.CompletedTask;
            if (this.users.Any(databaseUser => databaseUser.UserName == user.UserName))
            {
                return ServiceResult.AlreadyExists;
            }

            this.users.Add(user);
            return ServiceResult.Created;
        }

        /// <summary>
        ///     Read a user from the database.
        /// </summary>
        /// <param name="userName">The name of the user.</param>
        /// <returns>A <see cref="Task{T}" /> whose result is a <see cref="IUser" /> if the user exists and null otherwise.</returns>
        public async Task<IUser?> ReadAsync(string userName)
        {
            await Task.CompletedTask;
            return this.users.FirstOrDefault(databaseUser => databaseUser.UserName == userName);
        }

        /// <summary>
        ///     Checks if a user with the given name already exists.
        /// </summary>
        /// <param name="userName">The name of the user.</param>
        /// <returns>A <see cref="Task{T}" /> whose result is true if a user exists and false otherwise.</returns>
        public async Task<bool> UserExists(string userName)
        {
            await Task.CompletedTask;
            return this.users.Any(databaseUser => databaseUser.UserName == userName);
        }
    }
}
