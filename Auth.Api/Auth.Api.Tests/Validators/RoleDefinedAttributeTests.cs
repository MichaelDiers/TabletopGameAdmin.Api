namespace Auth.Api.Tests.Validators
{
    using Auth.Api.Contracts.Models;
    using Auth.Api.Validators;
    using Xunit;

    public class RoleDefinedAttributeTests
    {
        [Theory]
        [InlineData(Roles.None)]
        [InlineData((Roles) 100)]
        [InlineData("foo")]
        public void IsInvalid(object role)
        {
            var attribute = new RoleDefinedAttribute();
            Assert.False(attribute.IsValid(role));
        }

        [Theory]
        [InlineData(Roles.AuthSuperUser)]
        [InlineData(Roles.AuthAdmin)]
        [InlineData(Roles.All)]
        [InlineData(Roles.AuthUser)]
        public void IsValid(Roles role)
        {
            var attribute = new RoleDefinedAttribute();
            Assert.True(attribute.IsValid(role));
        }
    }
}
