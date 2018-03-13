using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Orders.Infrastructure.Commands.Interfaces;
using Orders.Infrastructure.Commands.Users;
using Orders.Infrastructure.Dtos;
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

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllAsync();

            return Ok(users);
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
            var token = await CommandDispatcher.DispatchAsync<Login, TokenDto>(command);
            
            return Ok(token);
        }
    }
}