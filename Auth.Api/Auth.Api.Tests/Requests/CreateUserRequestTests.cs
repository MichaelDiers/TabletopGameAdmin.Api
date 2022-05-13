namespace Auth.Api.Tests.Requests
{
    using System.Text.Json;
    using Auth.Api.Contracts.Models;
    using Auth.Api.Contracts.Requests;
    using Auth.Api.Requests;
    using Xunit;

    public class CreateUserRequestTests
    {
        [Theory]
        [InlineData(
            "email",
            "password",
            Roles.AuthSuperUser,
            "userName")]
        public void SerializeDeserialize(
            string email,
            string password,
            Roles role,
            string userName
        )
        {
            var request = new CreateUserRequest {Email = email, Password = password, Roles = role, UserName = userName};

            var actual =
                (ICreateUserRequest) JsonSerializer.Deserialize<CreateUserRequest>(JsonSerializer.Serialize(request));

            Assert.Equal(email, actual.Email);
            Assert.Equal(password, actual.Password);
            Assert.Equal(role, actual.Roles);
            Assert.Equal(userName, actual.UserName);
        }
    }
}
