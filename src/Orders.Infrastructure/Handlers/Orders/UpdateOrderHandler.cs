using System.Threading.Tasks;
using Orders.Infrastructure.Commands.Interfaces;
using Orders.Infrastructure.Commands.Orders;
using Orders.Infrastructure.Services.Interfaces;

namespace Orders.Infrastructure.Handlers.Orders
{
    public class UpdateOrderHandler : ServiceHandler, ICommandHandler<UpdateOrder>
    {
        public UpdateOrderHandler(IOrderService orderService) : base (orderService) { }
        public async Task HandleAsync(UpdateOrder command)
            => await _orderService.UpdateAsync(command.Id, command.Name, command.Status, command.Items);
    }
}