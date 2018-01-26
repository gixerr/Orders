using System.Threading.Tasks;
using Orders.Infrastructure.Commands.Interfaces;
using Orders.Infrastructure.Commands.Items;
using Orders.Infrastructure.Services.Interfaces;

namespace Orders.Infrastructure.Handlers.Items
{
    public class AddItemHandler : ServiceHandler, ICommandHandler<AddItem>
    {
        public AddItemHandler(IItemService itemService) : base(itemService) { }

        public async Task HandleAsync(AddItem command) 
            => await _itemService.AddAsync(command.itemName, command.categoryName);
    }
}