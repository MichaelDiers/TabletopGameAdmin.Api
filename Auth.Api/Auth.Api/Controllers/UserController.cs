namespace Auth.Api.Controllers
{
    using System.Net.Mime;
    using System.Threading.Tasks;
    using Auth.Api.Contracts.Models;
    using Auth.Api.Contracts.Services;
    using Auth.Api.Extensions;
    using Auth.Api.Requests;
    using Auth.Api.Responses;
    using Auth.Api.Services;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    ///     Controller for user authentication.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "AuthUser,AuthAdmin,AuthSuperUser")]
    public class UserController : ControllerBase
    {
        /// <summary>
        ///     The business logic of the controller.
        /// </summary>
        private readonly IUserService userService;

        /// <summary>
        ///     Create a new instance of the <see cref="UserController" /> class.
        /// </summary>
        /// <param name="userService">The business logic of the controller.</param>
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpDelete]
        [Authorize(Roles = "AuthUser,AuthAdmin,AuthSuperUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeleteUser([FromBody] DeleteUserRequest request)
        {
            // check if self delete or super user
            if (this.HttpContext.User.HasClaim(
                    claim => claim.Type == JwtService.UserNameClaimType &&
                             claim.Value.UserNameEquals(request.UserName)) ||
                this.HttpContext.User.IsInRole(nameof(Roles.AuthSuperUser)))
            {
                var result = await this.userService.DeleteUser(request);
                return result == ServiceResult.DocumentDeleted ? (ActionResult) new OkResult() : new NotFoundResult();
            }

            return new UnauthorizedResult();
        }

        /// <summary>
        ///     Signin an existing user and create a new json web token.
        /// </summary>
        /// <param name="request">The request data for signing in.</param>
        /// <returns>A <see cref="Task" /> whose result is an <see cref="ActionResult{T}" /> of <see cref="TokenResponse" />.</returns>
        [HttpPost]
        [AllowAnonymous]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TokenResponse>> SignInAsync([FromBody] SignInRequest request)
        {
            var tokenResponse = await this.userService.SignInAsync(request);
            if (string.IsNullOrWhiteSpace(tokenResponse.Token))
            {
                return new UnauthorizedResult();
            }

            return tokenResponse;
        }
    }
}
