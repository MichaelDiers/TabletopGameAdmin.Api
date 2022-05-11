namespace Auth.Api.Contracts.Models
{
    using System;

    /// <summary>
    ///     Describes the supported roles of the application.
    /// </summary>
    [Flags]
    public enum Roles
    {
        /// <summary>
        ///     No roles are set.
        /// </summary>
        None = 0,

        /// <summary>
        ///     User can modify its own user data.
        /// </summary>
        AuthUser = 1 << 1,

        /// <summary>
        ///     User can modify all users and set roles.
        /// </summary>
        AuthAdmin = 1 << 2,

        /// <summary>
        ///     User can create new admins.
        /// </summary>
        AuthSuperUser = 1 << 3
    }
}
