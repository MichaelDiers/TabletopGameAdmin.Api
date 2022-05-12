namespace Auth.Api.Models
{
    using Auth.Api.Contracts.Models;
    using Google.Cloud.Firestore;

    /// <summary>
    ///     Describes a user databaseService entity.
    /// </summary>
    [FirestoreData]
    public class User : IUser
    {
        /// <summary>
        ///     Creates a new instance of the <see cref="User" /> class.
        /// </summary>
        public User()
            : this(
                string.Empty,
                string.Empty,
                string.Empty,
                string.Empty,
                Roles.None)
        {
        }

        /// <summary>
        ///     Creates a new instance of the <see cref="User" /> class.
        /// </summary>
        /// <param name="userName">The name of the user.</param>
        /// <param name="password">The password of the user.</param>
        /// <param name="displayName">The display name of the user.</param>
        /// <param name="email">The email address of the user.</param>
        /// <param name="roles">The roles that are assigned to the user.</param>
        public User(
            string userName,
            string password,
            string displayName,
            string email,
            Roles roles
        )
        {
            this.DisplayName = displayName;
            this.Email = email;
            this.Roles = roles;
            this.Password = password;
            this.UserName = userName;
        }

        /// <summary>
        ///     Gets or sets the display name.
        /// </summary>
        [FirestoreProperty("displayName")]
        public string DisplayName { get; set; }

        /// <summary>
        ///     Gets or sets the email address of the user.
        /// </summary>
        [FirestoreProperty("email")]
        public string Email { get; set; }

        /// <summary>
        ///     Gets or sets the password of the user.
        /// </summary>
        [FirestoreProperty("password")]
        public string Password { get; set; }

        /// <summary>
        ///     Gets or sets the roles the user owns.
        /// </summary>
        [FirestoreProperty("roles")]
        public Roles Roles { get; set; }

        /// <summary>
        ///     Gets or sets the name of the user.
        /// </summary>
        [FirestoreProperty("userName")]
        public string UserName { get; set; }
    }
}
