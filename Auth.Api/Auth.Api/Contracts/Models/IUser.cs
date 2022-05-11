namespace Auth.Api.Contracts.Models
{
    /// <summary>
    ///     Describes a user databaseService entity.
    /// </summary>
    public interface IUser
    {
        /// <summary>
        ///     Gets the display name.
        /// </summary>
        string DisplayName { get; }

        /// <summary>
        ///     Gets the email address of the user.
        /// </summary>
        string Email { get; }

        /// <summary>
        ///     Gets the password of the user.
        /// </summary>
        string Password { get; }

        /// <summary>
        ///     Gets the roles the user owns.
        /// </summary>
        Roles Roles { get; }

        /// <summary>
        ///     Gets the name of the user.
        /// </summary>
        string UserName { get; }
    }
}
