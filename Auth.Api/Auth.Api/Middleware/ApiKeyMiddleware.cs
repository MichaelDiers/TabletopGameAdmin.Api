namespace Auth.Api.Middleware
{
    using System.Threading.Tasks;
    using Auth.Api.Contracts.Models;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    ///     Middleware validation for api keys.
    /// </summary>
    public class ApiKeyMiddleware
    {
        /// <summary>
        ///     A delegate for calling the next middleware.
        /// </summary>
        private readonly RequestDelegate next;

        /// <summary>
        ///     Creates a new instance of the <see cref="ApiKeyMiddleware" /> class.
        /// </summary>
        /// <param name="next">A delegate for calling the next middleware.</param>
        public ApiKeyMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        /// <summary>
        ///     Invoke the middleware and validate the api key.
        /// </summary>
        /// <param name="httpContext">The current <see cref="HttpContext" /> context.</param>
        /// <param name="appSettings">The settings of the application.</param>
        /// <returns>A <see cref="Task" />.</returns>
        public async Task InvokeAsync(HttpContext httpContext, IAppSettings appSettings)
        {
            if (httpContext.Request.Headers.TryGetValue(appSettings.ApiKeyName, out var apiKey) &&
                apiKey == appSettings.ApiKey)
            {
                await this.next(httpContext);
            }
            else
            {
                httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
            }
        }
    }
}
