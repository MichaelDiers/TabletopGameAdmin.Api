namespace Auth.Api.Models
{
    using System;
    using Auth.Api.Contracts.Models;

    /// <summary>
    ///     Describes a user databaseService entity.
    /// </summary>
    public class User : IUser
    {
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
            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(userName));
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(password));
            }

            if (string.IsNullOrWhiteSpace(displayName))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(displayName));
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(email));
            }

            if (roles == Roles.None)
            {
                throw new ArgumentException("Value cannot be Roles.None.", nameof(roles));
            }

            this.DisplayName = displayName;
            this.Email = email;
            this.Roles = roles;
            this.Password = password;
            this.UserName = userName;
        }

        /// <summary>
        ///     Gets or sets the display name.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        ///     Gets or sets the email address of the user.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        ///     Gets or sets the password of the user.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        ///     Gets or sets the roles the user owns.
        /// </summary>
        public Roles Roles { get; set; }

        /// <summary>
        ///     Gets or sets the name of the user.
        /// </summary>
        public string UserName { get; set; }
    }
}
