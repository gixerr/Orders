using System.Threading.Tasks;
using Orders.Infrastructure.Commands.Interfaces;
using Orders.Infrastructure.Commands.PreOrderItems;
using Orders.Infrastructure.Services.Interfaces;

namespace Orders.Infrastructure.Handlers.PreOrderItems
{
    public class AddItemHandler : ServiceHandler, ICommandHandler<AddItem>
    {
        public AddItemHandler(IPreOrderItemService preOrderItemService) : base(preOrderItemService) {}
    
        public async Task HandleAsync(AddItem command)
            =>await _preOrderItemService.AddAsync(command.ItemId, command.OrderName);
    }
}