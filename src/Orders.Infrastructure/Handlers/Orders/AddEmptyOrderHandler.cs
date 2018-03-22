using System.Threading.Tasks;
using Orders.Infrastructure.Commands.Interfaces;
using Orders.Infrastructure.Commands.Orders;
using Orders.Infrastructure.Services.Interfaces;

namespace Orders.Infrastructure.Handlers.Orders
{
    public class AddEmptyOrderHandler : ServiceHandler, ICommandHandler<AddEmptyOrder>
    {
        public AddEmptyOrderHandler(IOrderService orderService) : base(orderService) { }

        public async Task HandleAsync(AddEmptyOrder command)
            => await _orderService.AddAsync(command.Name);
    }
}