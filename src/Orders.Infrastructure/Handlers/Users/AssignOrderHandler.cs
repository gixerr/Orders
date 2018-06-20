using System.Threading.Tasks;
using Orders.Infrastructure.Commands.Interfaces;
using Orders.Infrastructure.Commands.Users;
using Orders.Infrastructure.Services.Interfaces;

namespace Orders.Infrastructure.Handlers.Users
{
    public class AssignOrderHandler : ServiceHandler, ICommandHandler<AssignOrder>
    {
        public AssignOrderHandler(IUserService userService) : base(userService)
        {
            
        }

        public Task HandleAsync(AssignOrder command)
            => _userService.AssignOrderToUserAsync(command.OrderId, command.UserId);
    }
}