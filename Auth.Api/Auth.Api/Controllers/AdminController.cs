﻿namespace Auth.Api.Controllers
{
    using System.Threading.Tasks;
    using Auth.Api.Contracts.Models;
    using Auth.Api.Contracts.Services;
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
        /// <returns>A <see cref="Task{T}" /> whose result indicates the operation result.</returns>
        [HttpPost]
        [Authorize(Roles = nameof(Roles.AuthSuperUser))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult> CreateUser([FromBody] CreateUserRequest request)
        {
            var result = await this.adminService.CreateUser(request);
            return result switch
            {
                ServiceResult.Created => new StatusCodeResult(StatusCodes.Status201Created),
                ServiceResult.AlreadyExists => new ConflictResult(),
                ServiceResult.MissingPrivileges => new UnauthorizedResult(),
                _ => new UnauthorizedResult()
            };
        }
    }
}