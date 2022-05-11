namespace Auth.Api.Contracts.Models
{
    /// <summary>
    ///     Describes the payload of a json web token.
    /// </summary>
    public interface IPayload
    {
        /// <summary>
        ///     Gets the display name of the user.
        /// </summary>
        string DisplayName { get; }

        /// <summary>
        ///     Gets the roles of the user.
        /// </summary>
        Roles Roles { get; }

        /// <summary>
        ///     Gets the name of the user.
        /// </summary>
        string UserName { get; }
    }
}
