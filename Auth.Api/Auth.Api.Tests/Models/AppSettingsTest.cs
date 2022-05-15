namespace Auth.Api.Tests.Models
{
    using Auth.Api.Contracts.Models;
    using Auth.Api.Models;
    using Xunit;

    /// <summary>
    ///     Tests for <see cref="AppSettings" />.
    /// </summary>
    public class AppSettingsTest
    {
        [Theory]
        [InlineData(
            "apiKey",
            "apiKeyName",
            "projectId",
            "userCollectionName")]
        public void Ctor(
            string apiKey,
            string apiKeyName,
            string projectId,
            string userCollectionName
        )
        {
            IAppSettings appSettings = new AppSettings
            {
                ApiKey = apiKey,
                ApiKeyName = apiKeyName,
                ProjectId = projectId,
                UserCollectionName = userCollectionName
            };

            Assert.Equal(apiKey, appSettings.ApiKey);
            Assert.Equal(apiKeyName, appSettings.ApiKeyName);
            Assert.Equal(projectId, appSettings.ProjectId);
            Assert.Equal(userCollectionName, appSettings.UserCollectionName);
        }
    }
}
