using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Orders.Infrastructure.Commands.Interfaces;
using Orders.Infrastructure.Commands.Users;
using Orders.Infrastructure.Services.Interfaces;

namespace Orders.Api.Controllers
{
    public class AccountsController : BaseController
    {
        private readonly IUserService _userService;

        public AccountsController(ICommandDispatcher commandDispatcher, IUserService userService) : base(commandDispatcher)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Register command)
        {
            await CommandDispatcher.DispatchAsync(command);

            return Created("accounts/", null);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Login command)
        {
            return Ok();
        }
    }
}