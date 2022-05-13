namespace Auth.Api.Contracts.Requests
{
    /// <summary>
    ///     Describes a delete user request.
    /// </summary>
    public interface IDeleteUserRequest
    {
        /// <summary>
        ///     Gets the name of the user that will be deleted.
        /// </summary>
        string UserName { get; }
    }
}
