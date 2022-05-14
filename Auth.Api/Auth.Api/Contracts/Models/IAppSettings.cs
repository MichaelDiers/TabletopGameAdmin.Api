namespace Auth.Api.Contracts.Models
{
    /// <summary>
    ///     Describes the application settings.
    /// </summary>
    public interface IAppSettings
    {
        /// <summary>
        ///     Gets the application key.
        /// </summary>
        string ApiKey { get; }

        /// <summary>
        ///     Gets the name of the application key.
        /// </summary>
        string ApiKeyName { get; }

        /// <summary>
        ///     Gets the id of the project.
        /// </summary>
        string ProjectId { get; }

        /// <summary>
        ///     Gets the name of the users database collection name.
        /// </summary>
        string UserCollectionName { get; }
    }
}
