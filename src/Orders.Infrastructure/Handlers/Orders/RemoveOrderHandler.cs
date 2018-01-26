using System.Threading.Tasks;
using Orders.Infrastructure.Commands.Handlers;
using Orders.Infrastructure.Commands.Interfaces;
using Orders.Infrastructure.Commands.Orders;
using Orders.Infrastructure.Services.Interfaces;

namespace Orders.Infrastructure.Handlers.Orders
{
    public class RemoveOrderHandler : ServiceHandler, ICommandHandler<RemoveOrder>
    {
        public RemoveOrderHandler(IOrderService orderService) : base(orderService) { }

        public async Task HandleAsync(RemoveOrder command)
            => await _orderService.RemoveAsync(command.Id); 
    }
}