namespace Auth.Api.Contracts.Requests
{
    using Auth.Api.Contracts.Models;

    /// <summary>
    ///     Request data for a new user.
    /// </summary>
    public interface ICreateUserRequest
    {
        /// <summary>
        ///     Gets the email of the user.
        /// </summary>
        string Email { get; }

        /// <summary>
        ///     Gets the password of the new user.
        /// </summary>
        string Password { get; }

        /// <summary>
        ///     Gets the roles of the new user.
        /// </summary>
        Roles Roles { get; }

        /// <summary>
        ///     Gets the name of the new user.
        /// </summary>
        string UserName { get; }
    }
}
