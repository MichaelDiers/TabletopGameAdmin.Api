namespace Auth.Api.Contracts.Requests
{
    /// <summary>
    ///     Describes a change password request.
    /// </summary>
    public interface IChangePasswordRequest : ISignInRequest
    {
        /// <summary>
        ///     Gets the new password.
        /// </summary>
        string NewPassword { get; }
    }
}
