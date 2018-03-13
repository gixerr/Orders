using System.Threading.Tasks;
using Orders.Infrastructure.Commands.Interfaces;
using Orders.Infrastructure.Commands.Users;
using Orders.Infrastructure.Dtos;
using Orders.Infrastructure.Services.Interfaces;

namespace Orders.Infrastructure.Handlers.Users
{
    public class LoginHandler : ServiceHandler, ICommandHandler<Login, TokenDto>
    {
        public LoginHandler(IUserService userService) : base(userService) { }

        public async Task<TokenDto> HandleAsync(Login command) 
            => await _userService.LoginAsync(command.Email, command.Password);
    }
}