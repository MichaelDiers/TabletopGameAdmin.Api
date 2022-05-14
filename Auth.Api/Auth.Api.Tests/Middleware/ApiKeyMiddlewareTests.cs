namespace Auth.Api.Tests.Middleware
{
    using System.Threading.Tasks;
    using Auth.Api.Middleware;
    using Auth.Api.Models;
    using Microsoft.AspNetCore.Http;
    using Xunit;

    /// <summary>
    ///     Tests for <see cref="ApiKeyMiddleware" />.
    /// </summary>
    public class ApiKeyMiddlewareTests
    {
        [Theory]
        [InlineData(
            true,
            "api:key",
            "api:key",
            StatusCodes.Status200OK)]
        [InlineData(
            false,
            "api:key",
            "api:key",
            StatusCodes.Status401Unauthorized)]
        [InlineData(
            true,
            "api:key",
            "api:key2",
            StatusCodes.Status401Unauthorized)]
        [InlineData(
            true,
            "api:key",
            "",
            StatusCodes.Status401Unauthorized)]
        public async Task InvokeAsync(
            bool setHeader,
            string appSettingsApiKey,
            string headerApiKey,
            int expectedStatusCode
        )
        {
            var apiKeyMiddleware = new ApiKeyMiddleware(context => Task.CompletedTask);
            var appSettings = new AppSettings {ApiKey = appSettingsApiKey, ApiKeyName = "apiKeyName"};
            var httpContext = new DefaultHttpContext();

            if (setHeader)
            {
                httpContext.Request.Headers.Add(appSettings.ApiKeyName, headerApiKey);
            }

            await apiKeyMiddleware.InvokeAsync(httpContext, appSettings);

            Assert.Equal(expectedStatusCode, httpContext.Response.StatusCode);
        }
    }
}
