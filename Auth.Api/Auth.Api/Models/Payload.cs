namespace Auth.Api.Models
{
    using System.Text.Json.Serialization;
    using Auth.Api.Contracts.Models;

    /// <summary>
    ///     Describes the payload of a json web token.
    /// </summary>
    public class Payload : IPayload
    {
        /// <summary>
        ///     Creates a new instance of the <see cref="Payload" /> class.
        /// </summary>
        public Payload()
        {
        }

        /// <summary>
        ///     Creates a new instance of the <see cref="Payload" /> class.
        /// </summary>
        /// <param name="user">The payload is created from the user data.</param>
        public Payload(IUser user)
        {
            this.Roles = user.Roles;
            this.UserName = user.UserName;
            this.DisplayName = user.DisplayName;
        }

        /// <summary>
        ///     Gets the display name of the user.
        /// </summary>
        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; } = string.Empty;

        /// <summary>
        ///     Gets the roles of the user.
        /// </summary>
        [JsonPropertyName("roles")]
        public Roles Roles { get; set; } = Roles.None;

        /// <summary>
        ///     Gets the name of the user.
        /// </summary>
        [JsonPropertyName("userName")]
        public string UserName { get; set; } = string.Empty;
    }
}
