namespace Auth.Api.Tests.Requests
{
    using System.Text.Json;
    using Auth.Api.Contracts.Requests;
    using Auth.Api.Requests;
    using Xunit;

    public class SignInRequestTests
    {
        [Theory]
        [InlineData("userName", "password")]
        public void SerializeDeserialize(string userName, string password)
        {
            var request = new SignInRequest(userName, password);
            var actual = (ISignInRequest) JsonSerializer.Deserialize<SignInRequest>(JsonSerializer.Serialize(request));
            Assert.Equal(userName, actual.UserName);
            Assert.Equal(password, actual.Password);
        }
    }
}
