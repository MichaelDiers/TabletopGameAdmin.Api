namespace Auth.Api.Requests
{
    using System.ComponentModel.DataAnnotations;
    using System.Text.Json.Serialization;
    using Auth.Api.Contracts.Requests;

    /// <summary>
    ///     Describes a sign in request.
    /// </summary>
    public class SignInRequest : ISignInRequest

    {
        /// <summary>
        ///     Creates a new instance of the <see cref="SignInRequest" /> class.
        /// </summary>
        public SignInRequest()
            : this(string.Empty, string.Empty)
        {
        }

        /// <summary>
        ///     Creates a new instance of the <see cref="SignInRequest" /> class.
        /// </summary>
        /// <param name="userName">The name of the user.</param>
        /// <param name="password">The password of the user.</param>
        public SignInRequest(string userName, string password)
        {
            this.UserName = userName;
            this.Password = password;
        }

        /// <summary>
        ///     Gets or sets the password.
        /// </summary>
        [JsonPropertyName("password")]
        [Required]
        public string Password { get; set; }

        /// <summary>
        ///     Gets or sets the name of the user.
        /// </summary>
        [JsonPropertyName("userName")]
        [Required]
        public string UserName { get; set; }
    }
}
