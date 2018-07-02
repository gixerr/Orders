using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Orders.Infrastructure.Commands.Interfaces;
using Orders.Infrastructure.Commands.RefreshTokens;
using Orders.Infrastructure.Commands.Users;
using Orders.Infrastructure.Dtos;
using Orders.Infrastructure.Policies;
using Orders.Infrastructure.Services.Interfaces;

namespace Orders.Api.Controllers
{
    public class AccountsController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IRefreshTokenService _refreshTokenService;

        public AccountsController(ICommandDispatcher commandDispatcher, IUserService userService,
            IRefreshTokenService refreshTokenService) : base(commandDispatcher)
        {
            _userService = userService;
            _refreshTokenService = refreshTokenService;
        }

        [HttpGet]
        [Authorize(Policy = Policy.AdminOnly)]
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

        [HttpPost("tokens/refresh")]
        public IActionResult RefreshAccessToken([FromBody] RefreshAccessToken command)
        {
            var token = _refreshTokenService.RefreshAccessToken(command.RefreshToken);

            return Ok(token);
        }

        [HttpPost("tokens/revoke")]
        public IActionResult RevokeRefreshToken([FromBody] RevokeRefreshToken command)
        {
            _refreshTokenService.RevokeRefreshToken(command.RefreshToken);

            return NoContent();
        }
    }
}