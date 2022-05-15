namespace Auth.Api.Contracts.Services
{
    /// <summary>
    ///     Describes the result of a service.
    /// </summary>
    public enum ServiceResult
    {
        /// <summary>
        ///     Undefined result.
        /// </summary>
        None = 0,

        /// <summary>
        ///     Indicates that an entity is created.
        /// </summary>
        Created = 201,

        /// <summary>
        ///     Indicates that an entity already exists.
        /// </summary>
        Conflict = 409,

        /// <summary>
        ///     Indicates that a document is deleted.
        /// </summary>
        Deleted = 204,

        /// <summary>
        ///     Indicates a successful update.
        /// </summary>
        Updated = 204,

        /// <summary>
        ///     Indicates document not found.
        /// </summary>
        NotFound = 404
    }
}
