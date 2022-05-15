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
        Created = 1,

        /// <summary>
        ///     Indicates that an entity already exists.
        /// </summary>
        AlreadyExists = 2,

        /// <summary>
        ///     User is not allowed to execute the operation.
        /// </summary>
        MissingPrivileges = 3,

        /// <summary>
        ///     Indicates that a document is deleted.
        /// </summary>
        DocumentDeleted = 4,

        /// <summary>
        ///     Indicates that a document does not exists.
        /// </summary>
        DocumentDoesNotExists = 5,

        /// <summary>
        ///     Indicates an internal server error.
        /// </summary>
        InternalServerError = 6,

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
