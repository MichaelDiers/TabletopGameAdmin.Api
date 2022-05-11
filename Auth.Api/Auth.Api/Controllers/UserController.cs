namespace Auth.Api.Controllers
{
    using System.Net.Mime;
    using System.Threading.Tasks;
    using Auth.Api.Contracts.Services;
    using Auth.Api.Requests;
    using Auth.Api.Responses;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    ///     Controller for user authentication.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
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

        /// <summary>
        ///     Signin an existing user and create a new json web token.
        /// </summary>
        /// <param name="request">The request data for signing in.</param>
        /// <returns>A <see cref="Task" /> whose result is an <see cref="ActionResult{T}" /> of <see cref="TokenResponse" />.</returns>
        [HttpPost]
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
