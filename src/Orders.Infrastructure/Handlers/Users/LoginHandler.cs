using System.Threading.Tasks;
using Orders.Infrastructure.Commands.Interfaces;
using Orders.Infrastructure.Commands.Users;
using Orders.Infrastructure.Services.Interfaces;

namespace Orders.Infrastructure.Handlers.Users
{
    public class LoginHandler : ServiceHandler, ICommandHandler<Login>
    {
        public LoginHandler(IUserService userService) : base(userService) { }
        public Task HandleAsync(Login command)
        {
            throw new System.NotImplementedException();
        }
    }
}