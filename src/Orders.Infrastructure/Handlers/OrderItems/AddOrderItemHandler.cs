using System.Threading.Tasks;
using Orders.Infrastructure.Commands.Interfaces;
using Orders.Infrastructure.Commands.OrderItems;
using Orders.Infrastructure.Commands.Orders;
using Orders.Infrastructure.Services.Interfaces;

namespace Orders.Infrastructure.Handlers.OrderItems
{
    public class AddOrderItemHandler : ServiceHandler, ICommandHandler<AddOrderItem>
    {
        public AddOrderItemHandler(IOrderItemService orderItemService) : base (orderItemService) { }

        public async Task HandleAsync(AddOrderItem command)
            => await _orderItemService.AddAsync(command.OrderId, command.ItemId);
    }
}