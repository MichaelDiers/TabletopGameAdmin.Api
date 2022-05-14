namespace Auth.Api.Models
{
    using Auth.Api.Contracts.Models;

    /// <summary>
    ///     Describes the application settings.
    /// </summary>
    public class AppSettings : IAppSettings
    {
        /// <summary>
        ///     Gets or sets the application key.
        /// </summary>
        public string ApiKey { get; set; } = string.Empty;

        /// <summary>
        ///     Gets or sets the name of the application key.
        /// </summary>
        public string ApiKeyName { get; set; } = string.Empty;

        /// <summary>
        ///     Gets or sets the id of the project.
        /// </summary>
        public string ProjectId { get; set; } = string.Empty;

        /// <summary>
        ///     Gets or sets the name of the users database collection name.
        /// </summary>
        public string UserCollectionName { get; set; } = string.Empty;
    }
}
