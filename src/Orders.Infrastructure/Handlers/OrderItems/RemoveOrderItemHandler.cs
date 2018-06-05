using System.Threading.Tasks;
using Orders.Infrastructure.Commands.Interfaces;
using Orders.Infrastructure.Commands.OrderItems;
using Orders.Infrastructure.Services.Interfaces;

namespace Orders.Infrastructure.Handlers.OrderItems
{
    public class RemoveOrderItemHandler : ServiceHandler, ICommandHandler<RemoveOrderItem>
    {
        public RemoveOrderItemHandler(IOrderItemService orderItemService) : base(orderItemService)
        {
        }

        public async Task HandleAsync(RemoveOrderItem command)
            => await _orderItemService.RemoveAsync(command.OrderId, command.ItemId);
    }
}