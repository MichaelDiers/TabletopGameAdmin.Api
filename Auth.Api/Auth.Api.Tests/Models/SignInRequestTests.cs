namespace Auth.Api.Tests.Models
{
    using System.Text.Json;
    using Auth.Api.Requests;
    using Xunit;

    public class SignInRequestTests
    {
        [Fact]
        public void DefaultCtor()
        {
            var actual = new SignInRequest();
            Assert.Equal(string.Empty, actual.Password);
            Assert.Equal(string.Empty, actual.UserName);
        }

        [Theory]
        [InlineData("userName", "password")]
        public void SerializeDeserialize(string userName, string password)
        {
            var request = new SignInRequest(userName, password);

            var json = JsonSerializer.Serialize(request);
            var deserialized = JsonSerializer.Deserialize<SignInRequest>(json);

            Assert.Equal(userName, deserialized.UserName);
            Assert.Equal(password, deserialized.Password);
        }
    }
}
