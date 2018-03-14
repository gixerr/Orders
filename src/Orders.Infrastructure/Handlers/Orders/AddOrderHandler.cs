using System.Threading.Tasks;
using Orders.Infrastructure.Commands.Interfaces;
using Orders.Infrastructure.Commands.Orders;
using Orders.Infrastructure.Services.Interfaces;

namespace Orders.Infrastructure.Handlers.Orders
{
    public class AddOrderHandler : ServiceHandler, ICommandHandler<AddOrder>
    {
        public AddOrderHandler(IOrderService orderService) : base(orderService) { }

        public async Task HandleAsync(AddOrder command)
            => await _orderService.AddAsync(command.Name);
    }
}