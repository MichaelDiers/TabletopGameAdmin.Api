namespace Auth.Api.Extensions
{
    using System;
    using Auth.Api.Contracts.Services;
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
            if (result == ServiceResult.None ||
                !Enum.IsDefined(typeof(ServiceResult), result) ||
                (int) result < 100 ||
                (int) result > 600)
            {
                throw new ArgumentException($"Value not supported: {result}", nameof(result));
            }

            return new StatusCodeResult((int) result);
        }
    }
}
