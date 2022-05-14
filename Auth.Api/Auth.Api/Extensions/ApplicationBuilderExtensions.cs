namespace Auth.Api.Extensions
{
    using Auth.Api.Middleware;
    using Microsoft.AspNetCore.Builder;

    /// <summary>
    ///     Extensions for <see cref="IApplicationBuilder " />.
    /// </summary>
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        ///     Add the <see cref="ApiKeyMiddleware" /> to the builder.
        /// </summary>
        /// <param name="builder">The current application builder.</param>
        /// <returns>The given <paramref name="builder" />.</returns>
        public static IApplicationBuilder UseApiKeyMiddleware(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<ApiKeyMiddleware>();
            return builder;
        }
    }
}
