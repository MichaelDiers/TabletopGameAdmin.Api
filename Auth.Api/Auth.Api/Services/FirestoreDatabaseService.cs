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
        ///     Create a new user in the database.
        /// </summary>
        /// <param name="user">The user data to be created.</param>
        /// <returns>
        ///     A <see cref="Task{T}" /> whose result is <see cref="ServiceResult.Created" /> if the new user is created and
        ///     <see cref="ServiceResult.AlreadyExists" /> otherwise.
        /// </returns>
        public async Task<ServiceResult> CreateUser(IUser user)
        {
            try
            {
                var documentReference = this.collectionReference.Document(user.UserName);
                await documentReference.CreateAsync(user);
                return ServiceResult.Created;
            }
            catch
            {
                return ServiceResult.AlreadyExists;
            }
        }

        /// <summary>
        ///     Delete all generic test users.
        /// </summary>
        /// <returns>A <see cref="ServiceResult.DocumentDeleted" /> or <see cref="ServiceResult.DocumentDoesNotExists" />.</returns>
        public async Task<ServiceResult> DeleteGenericUsers()
        {
            var snapshots = await this.collectionReference.GetSnapshotAsync();
            if (snapshots.Count == 0)
            {
                return ServiceResult.DocumentDoesNotExists;
            }

            var batch = this.collectionReference.Database.StartBatch();
            foreach (var snapshot in snapshots.Where(snapshot => snapshot.Id.StartsWith("POSTMAN_")))
            {
                batch.Delete(snapshot.Reference);
            }

            await batch.CommitAsync();
            return ServiceResult.DocumentDeleted;
        }

        /// <summary>
        ///     Delete a user by name of the user.
        /// </summary>
        /// <param name="userName">The name of the user.</param>
        /// <returns>
        ///     A <see cref="Task{T}" /> whose result is <see cref="ServiceResult.DocumentDeleted" /> or
        ///     <see cref="ServiceResult.DocumentDoesNotExists" />.
        /// </returns>
        public async Task<ServiceResult> DeleteUser(string userName)
        {
            try
            {
                await this.collectionReference.Document(userName).DeleteAsync(Precondition.MustExist);
                return ServiceResult.DocumentDeleted;
            }
            catch (RpcException ex) when (ex.StatusCode == StatusCode.NotFound)
            {
                return ServiceResult.DocumentDoesNotExists;
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
        public async Task<bool> UserExists(string userName)
        {
            var snapshot = await this.collectionReference.Document(userName).GetSnapshotAsync();
            return snapshot.Exists;
        }
    }
}
