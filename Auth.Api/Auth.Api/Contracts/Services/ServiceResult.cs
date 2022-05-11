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
        MissingPrivileges = 3
    }
}
