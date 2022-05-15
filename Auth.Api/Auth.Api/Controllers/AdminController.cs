namespace Auth.Api.Controllers
{
    using System.Threading.Tasks;
    using Auth.Api.Contracts.Models;
    using Auth.Api.Contracts.Services;
    using Auth.Api.Extensions;
    using Auth.Api.Requests;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    ///     Describes the administration routes for user authentication.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AdminController : ControllerBase
    {
        /// <summary>
        ///     Business logic of the controller.
        /// </summary>
        private readonly IAdminService adminService;

        /// <summary>
        ///     Creates a new instance of the <see cref="AdminController" /> class.
        /// </summary>
        /// <param name="adminService">Business logic of the controller.</param>
        public AdminController(IAdminService adminService)
        {
            this.adminService = adminService;
        }

        /// <summary>
        ///     Create a new user.
        /// </summary>
        /// <param name="request">The data of the new user.</param>
        /// <returns>A <see cref="Task{TResult}" /> whose result indicates the operation result.</returns>
        [HttpPost]
        [Authorize(Roles = "AuthAdmin,AuthSuperUser")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult> CreateUser([FromBody] CreateUserRequest request)
        {
            if (((request.Roles & Roles.AuthAdmin) == Roles.AuthAdmin ||
                 (request.Roles & Roles.AuthSuperUser) == Roles.AuthSuperUser) &&
                !this.HttpContext.User.IsInRole(nameof(Roles.AuthSuperUser)))
            {
                return new BadRequestResult();
            }

            var result = await this.adminService.CreateUser(request);
            return result.ToActionResult();
        }

        /// <summary>
        ///     Delete all generic test users.
        /// </summary>
        /// <returns>A <see cref="Task{TResult}" /> whose result indicates the operation result.</returns>
        [HttpDelete]
        [Authorize(Roles = nameof(Roles.AuthSuperUser))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteGenericUsersAsync()
        {
            var result = await this.adminService.DeleteGenericUsersAsync();
            return result.ToActionResult();
        }
    }
}
