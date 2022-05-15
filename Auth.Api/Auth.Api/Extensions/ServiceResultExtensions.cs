namespace Auth.Api.Extensions
{
    using System;
    using Auth.Api.Contracts.Services;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    ///     Extensions for <see cref="ServiceResult" />.
    /// </summary>
    public static class ServiceResultExtensions
    {
        /// <summary>
        ///     Map a <see cref="ServiceResult" /> to a matching <see cref="ActionResult" />.
        /// </summary>
        /// <param name="result">The service that is mapped.</param>
        /// <returns>A matching <see cref="ActionResult" />.</returns>
        public static ActionResult ToActionResult(this ServiceResult result)
        {
            if ((int) result > 100)
            {
                return new StatusCodeResult((int) result);
            }

            return result switch
            {
                ServiceResult.None => new StatusCodeResult(StatusCodes.Status500InternalServerError),
                ServiceResult.Created => new StatusCodeResult(StatusCodes.Status201Created),
                ServiceResult.AlreadyExists => new ConflictResult(),
                ServiceResult.MissingPrivileges => new ForbidResult(),
                ServiceResult.DocumentDeleted => new OkResult(),
                ServiceResult.DocumentDoesNotExists => new NotFoundResult(),
                ServiceResult.InternalServerError => new StatusCodeResult(StatusCodes.Status500InternalServerError),
                _ => throw new ArgumentOutOfRangeException(nameof(result), result, null)
            };
        }
    }
}
