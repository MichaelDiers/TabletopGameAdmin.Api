namespace Auth.Api.Tests.Services
{
    using System;
    using Auth.Api.Contracts.Services;
    using Auth.Api.Services;
    using Xunit;

    /// <summary>
    ///     Tests for <see cref="PasswordHashService" />.
    /// </summary>
    public class PasswordHashTests
    {
        [Theory]
        [InlineData("password")]
        public void HashAndVerify(string password)
        {
            var service = new PasswordHashService() as IPasswordHashService;
            var hash = service.Hash(password);
            Assert.True(service.VerifyHash(password, hash));
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("\n")]
        public void HashThrowsArgumentExceptionForWhiteSpacePasswords(string password)
        {
            var service = new PasswordHashService() as IPasswordHashService;
            Assert.Throws<ArgumentException>(() => service.Hash(password));
        }

        [Fact]
        public void VerifyFailsIfHashDoesNotMatch()
        {
            var service = new PasswordHashService() as IPasswordHashService;
            var hash = service.Hash(Guid.NewGuid().ToString());
            Assert.False(service.VerifyHash(Guid.NewGuid().ToString(), hash));
        }

        [Theory]
        [InlineData("", "hash")]
        [InlineData(" ", "hash")]
        [InlineData("\n", "hash")]
        [InlineData("password", "")]
        [InlineData("password", " ")]
        [InlineData("password", "\n")]
        [InlineData("", "")]
        [InlineData(" ", " ")]
        [InlineData("\n", "\n")]
        public void VerifyThrowsArgumentExceptionForWhiteSpacePasswordsAndHashes(string password, string hash)
        {
            var service = new PasswordHashService() as IPasswordHashService;
            Assert.Throws<ArgumentException>(() => service.VerifyHash(password, hash));
        }
    }
}
