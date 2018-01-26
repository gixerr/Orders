using System.Threading.Tasks;
using Orders.Infrastructure.Commands.Interfaces;
using Orders.Infrastructure.Commands.Items;
using Orders.Infrastructure.Services.Interfaces;

namespace Orders.Infrastructure.Handlers.Items
{
    public class RemoveItemHandler : ServiceHandler, ICommandHandler<RemoveItem>
    {
        public RemoveItemHandler(IItemService itemService) : base(itemService) { }
        public async Task HandleAsync(RemoveItem command) 
            => await _itemService.RemoveAsync(command.Id);
    }
}