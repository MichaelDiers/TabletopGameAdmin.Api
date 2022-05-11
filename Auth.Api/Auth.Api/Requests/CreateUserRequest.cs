namespace Auth.Api.Requests
{
    using System.ComponentModel.DataAnnotations;
    using System.Text.Json.Serialization;
    using Auth.Api.Contracts.Models;
    using Auth.Api.Contracts.Requests;

    /// <summary>
    ///     Request data for a new user.
    /// </summary>
    public class CreateUserRequest : ICreateUserRequest
    {
        /// <summary>
        ///     Gets or sets the email of the user.
        /// </summary>
        [JsonPropertyName("email")]
        [Required]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        ///     Gets or sets the password of the new user.
        /// </summary>
        [JsonPropertyName("password")]
        [Required]
        public string Password { get; set; } = string.Empty;

        /// <summary>
        ///     Gets or sets the roles of the new user.
        /// </summary>
        [JsonPropertyName("roles")]
        [Required]
        public Roles Roles { get; set; } = Roles.None;

        /// <summary>
        ///     Gets or sets the name of the new user.
        /// </summary>
        [JsonPropertyName("userName")]
        [Required]
        public string UserName { get; set; } = string.Empty;
    }
}
