namespace Auth.Api.Tests.Models
{
    using System.Text.Json;
    using Auth.Api.Contracts.Models;
    using Auth.Api.Models;
    using Xunit;

    /// <summary>
    ///     Tests for <see cref="User" />.
    /// </summary>
    public class UserTests
    {
        [Fact]
        public void DefaultCtor()
        {
            var actual = new User();
            Assert.Equal(string.Empty, actual.UserName);
            Assert.Equal(string.Empty, actual.Password);
            Assert.Equal(string.Empty, actual.DisplayName);
            Assert.Equal(string.Empty, actual.Email);
            Assert.Equal(Roles.None, actual.Roles);
        }

        [Theory]
        [InlineData(
            "userName",
            "password",
            "displayName",
            "email",
            Roles.AuthAdmin | Roles.AuthSuperUser)]
        public void SerializeDeserialize(
            string userName,
            string password,
            string displayName,
            string email,
            Roles roles
        )
        {
            var user = new User(
                userName,
                password,
                displayName,
                email,
                roles);
            var actual = (IUser) JsonSerializer.Deserialize<User>(JsonSerializer.Serialize(user));
            Assert.Equal(userName, actual.UserName);
            Assert.Equal(password, actual.Password);
            Assert.Equal(displayName, actual.DisplayName);
            Assert.Equal(email, actual.Email);
            Assert.Equal(roles, actual.Roles);
        }
    }
}
