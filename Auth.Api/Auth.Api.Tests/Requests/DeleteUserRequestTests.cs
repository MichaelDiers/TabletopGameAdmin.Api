namespace Auth.Api.Tests.Requests
{
    using System.Text.Json;
    using Auth.Api.Contracts.Requests;
    using Auth.Api.Requests;
    using Xunit;

    public class DeleteUserRequestTests
    {
        [Theory]
        [InlineData("userName")]
        public void SerializeDeserialize(string userName)
        {
            var request = new DeleteUserRequest {UserName = userName};
            var actual =
                (IDeleteUserRequest) JsonSerializer.Deserialize<DeleteUserRequest>(JsonSerializer.Serialize(request));
            Assert.Equal(userName, actual.UserName);
        }
    }
}
