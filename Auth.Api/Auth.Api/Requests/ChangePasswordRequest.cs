namespace Auth.Api.Requests
{
    using System.ComponentModel.DataAnnotations;
    using System.Text.Json.Serialization;
    using Auth.Api.Contracts.Requests;

    /// <summary>
    ///     Describes a change password request.
    /// </summary>
    public class ChangePasswordRequest : SignInRequest, IChangePasswordRequest
    {
        /// <summary>
        ///     Creates a new instance of the <see cref="ChangePasswordRequest" /> class.
        /// </summary>
        public ChangePasswordRequest()
            : this(string.Empty, string.Empty, string.Empty)
        {
        }

        /// <summary>
        ///     Creates a new instance of the <see cref="ChangePasswordRequest" /> class.
        /// </summary>
        /// <param name="userName">The name of the user.</param>
        /// <param name="password">The password of the user.</param>
        /// <param name="newPassword">The new password of the user.</param>
        public ChangePasswordRequest(string userName, string password, string newPassword)
        {
            this.UserName = userName;
            this.Password = password;
            this.NewPassword = newPassword;
        }

        /// <summary>
        ///     Gets or sets the new password.
        /// </summary>
        [Required]
        [JsonPropertyName("newPassword")]
        public string NewPassword { get; set; }
    }
}
