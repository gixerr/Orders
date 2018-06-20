using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Orders.Infrastructure.Commands.Interfaces;
using Orders.Infrastructure.Commands.Users;
using Orders.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System;

namespace Orders.Api.Controllers
{
    public class UsersController : BaseController
    {
        private readonly IUserService _userService;

        public UsersController(ICommandDispatcher commandDispatcher, IUserService userService) : base(commandDispatcher)
        {
            _userService = userService;
        }

        [HttpPut("{orderId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> AssignOrder(Guid orderId)
        {
            await _userService.AssignOrderToUserAsync(orderId, UserId);

            return Ok();
        }
    }
}