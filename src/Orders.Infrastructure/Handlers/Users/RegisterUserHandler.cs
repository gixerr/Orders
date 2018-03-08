using System.Threading.Tasks;
using Orders.Infrastructure.Commands.Interfaces;
using Orders.Infrastructure.Commands.Users;
using Orders.Infrastructure.Services.Interfaces;

namespace Orders.Infrastructure.Handlers.Users
{
    public class RegisterHandler : ServiceHandler, ICommandHandler<Register>
    {
        public RegisterHandler(IUserService userService) : base(userService) { }
        public async Task HandleAsync(Register command)
            => await _userService.RegisterAsync(command.Id, command.Name, command.Email, 
                command.Role, command.Password, command.Salt);
    }
}