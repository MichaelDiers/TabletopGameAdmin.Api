namespace Auth.Api.Contracts.Requests
{
    /// <summary>
    ///     Describes a sign in request.
    /// </summary>
    public interface ISignInRequest
    {
        /// <summary>
        ///     Gets the password.
        /// </summary>
        string Password { get; }

        /// <summary>
        ///     Gets the name of the user.
        /// </summary>
        string UserName { get; }
    }
}
