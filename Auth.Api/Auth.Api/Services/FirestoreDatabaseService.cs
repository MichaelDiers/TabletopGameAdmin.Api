namespace Auth.Api.Services
{
    using System.Linq;
    using System.Threading.Tasks;
    using Auth.Api.Contracts.Models;
    using Auth.Api.Contracts.Services;
    using Auth.Api.Models;
    using Google.Cloud.Firestore;
    using Grpc.Core;

    /// <summary>
    ///     Access to the firestore database.
    /// </summary>
    public class FirestoreDatabaseService : IDatabaseService
    {
        /// <summary>
        ///     A reference to the used database collection.
        /// </summary>
        private readonly CollectionReference collectionReference;

        /// <summary>
        ///     Creates a new instance of the <see cref="FirestoreDatabaseService" /> class.
        /// </summary>
        /// <param name="collectionReference">A reference to the database collection.</param>
        public FirestoreDatabaseService(CollectionReference collectionReference)
        {
            this.collectionReference = collectionReference;
        }

        /// <summary>
        ///     Update the password for a user.
        /// </summary>
        /// <param name="userName">The name of the user.</param>
        /// <param name="password">The new password.</param>
        /// <returns>A <see cref="Task" />.</returns>
        public async Task ChangePassword(string userName, string password)
        {
            await this.collectionReference.Document(userName).UpdateAsync(User.PasswordName, password);
        }

        /// <summary>
        ///     Create a new user in the database.
        /// </summary>
        /// <param name="user">The user data to be created.</param>
        /// <returns>
        ///     A <see cref="Task{T}" /> whose result is <see cref="ServiceResult.Created" /> if the new user is created and
        ///     <see cref="ServiceResult.Conflict" /> otherwise.
        /// </returns>
        public async Task<ServiceResult> CreateUserAsync(IUser user)
        {
            try
            {
                var documentReference = this.collectionReference.Document(user.UserName);
                await documentReference.CreateAsync(user);
                return ServiceResult.Created;
            }
            catch
            {
                return ServiceResult.Conflict;
            }
        }

        /// <summary>
        ///     Delete all generic test users.
        /// </summary>
        /// <returns>A <see cref="ServiceResult.Deleted" /> or <see cref="ServiceResult.NotFound" />.</returns>
        public async Task<ServiceResult> DeleteGenericUsersAsync()
        {
            var snapshots = await this.collectionReference.GetSnapshotAsync();
            if (snapshots.Count == 0)
            {
                return ServiceResult.NotFound;
            }

            var batch = this.collectionReference.Database.StartBatch();
            foreach (var snapshot in snapshots.Where(snapshot => snapshot.Id.StartsWith("POSTMAN_")))
            {
                batch.Delete(snapshot.Reference);
            }

            await batch.CommitAsync();
            return ServiceResult.Deleted;
        }

        /// <summary>
        ///     Delete a user by name of the user.
        /// </summary>
        /// <param name="userName">The name of the user.</param>
        /// <returns>
        ///     A <see cref="Task{T}" /> whose result is <see cref="ServiceResult.Deleted" /> or
        ///     <see cref="ServiceResult.NotFound" />.
        /// </returns>
        public async Task<ServiceResult> DeleteUserAsync(string userName)
        {
            try
            {
                await this.collectionReference.Document(userName).DeleteAsync(Precondition.MustExist);
                return ServiceResult.Deleted;
            }
            catch (RpcException ex) when (ex.StatusCode == StatusCode.NotFound)
            {
                return ServiceResult.NotFound;
            }
        }

        /// <summary>
        ///     Read a user from the database.
        /// </summary>
        /// <param name="userName">The name of the user.</param>
        /// <returns>A <see cref="Task{T}" /> whose result is a <see cref="IUser" /> if the user exists and null otherwise.</returns>
        public async Task<IUser?> ReadAsync(string userName)
        {
            var document = await this.collectionReference.Document(userName).GetSnapshotAsync();
            return !document.Exists ? null : document.ConvertTo<User>();
        }

        /// <summary>
        ///     Checks if a user with the given name already exists.
        /// </summary>
        /// <param name="userName">The name of the user.</param>
        /// <returns>A <see cref="Task{T}" /> whose result is true if a user exists and false otherwise.</returns>
        public async Task<bool> UserExistsAsync(string userName)
        {
            var snapshot = await this.collectionReference.Document(userName).GetSnapshotAsync();
            return snapshot.Exists;
        }
    }
}
