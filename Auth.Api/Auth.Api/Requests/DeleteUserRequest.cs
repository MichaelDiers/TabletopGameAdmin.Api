namespace Auth.Api.Requests
{
    using System.ComponentModel.DataAnnotations;
    using System.Text.Json.Serialization;
    using Auth.Api.Contracts.Requests;

    /// <summary>
    ///     Describes a delete user request.
    /// </summary>
    public class DeleteUserRequest : IDeleteUserRequest
    {
        /// <summary>
        ///     Gets or sets the name of the user that will be deleted.
        /// </summary>
        [JsonPropertyName("userName")]
        [Required]
        public string UserName { get; set; } = string.Empty;
    }
}
