namespace Auth.Api.Tests.Requests
{
    using System.Text.Json;
    using Auth.Api.Contracts.Requests;
    using Auth.Api.Requests;
    using Xunit;

    /// <summary>
    ///     Tests for <see cref="ChangePasswordRequest" />.
    /// </summary>
    public class ChangePasswordRequestTests
    {
        [Fact]
        public void DefaultCtor()
        {
            var request = new ChangePasswordRequest();

            var actual =
                (IChangePasswordRequest) JsonSerializer.Deserialize<ChangePasswordRequest>(
                    JsonSerializer.Serialize(request));

            Assert.Equal(string.Empty, actual.UserName);
            Assert.Equal(string.Empty, actual.Password);
            Assert.Equal(string.Empty, actual.NewPassword);
        }

        [Theory]
        [InlineData("userName", "password", "newPassword")]
        public void SerializeDeserialize(string userName, string password, string newPassword)
        {
            var request = new ChangePasswordRequest(userName, password, newPassword);

            var actual =
                (IChangePasswordRequest) JsonSerializer.Deserialize<ChangePasswordRequest>(
                    JsonSerializer.Serialize(request));

            Assert.Equal(userName, actual.UserName);
            Assert.Equal(password, actual.Password);
            Assert.Equal(newPassword, actual.NewPassword);
        }
    }
}
