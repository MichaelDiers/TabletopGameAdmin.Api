namespace Auth.Api.Tests.Responses
{
    using Auth.Api.Responses;
    using Xunit;

    public class TokenResponseTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("token")]
        public void Ctor(string? token)
        {
            var response = new TokenResponse(token);
            Assert.Equal(token, response.Token);
        }
    }
}
