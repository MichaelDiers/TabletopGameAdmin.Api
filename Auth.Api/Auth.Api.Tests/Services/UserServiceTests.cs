namespace Auth.Api.Tests.Services
{
    using System.Threading.Tasks;
    using Auth.Api.Contracts.Models;
    using Auth.Api.Contracts.Services;
    using Auth.Api.Models;
    using Auth.Api.Requests;
    using Auth.Api.Services;
    using NSubstitute;
    using Xunit;

    public class UserServiceTests
    {
        [Theory]
        [InlineData("userName", "password", true)]
        [InlineData("userName1", "password1", false)]
        public async void SignInAsync(string userName, string password, bool createToken)
        {
            var databaseService = Substitute.For<IDatabaseService>();
            databaseService.ReadAsync(Arg.Any<string>())
                .Returns(
                    Task.FromResult<IUser?>(
                        createToken
                            ? new User(
                                userName,
                                password,
                                userName,
                                "email",
                                Roles.AuthUser)
                            : null));

            var jwtService = Substitute.For<IJwtService>();
            jwtService.CreateAsync(Arg.Is<IPayload>(payload => payload.DisplayName == userName))
                .Returns(Task.FromResult(createToken ? "token" : string.Empty));

            var service = new UserService(databaseService, jwtService);
            var tokenResponse = await service.SignInAsync(new SignInRequest(password, userName));
            Assert.NotNull(tokenResponse);
            Assert.True(
                createToken
                    ? !string.IsNullOrWhiteSpace(tokenResponse.Token)
                    : string.IsNullOrWhiteSpace(tokenResponse.Token));
        }
    }
}
