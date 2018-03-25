using System.Threading.Tasks;
using Orders.Infrastructure.Commands.Interfaces;
using Orders.Infrastructure.Commands.PreOrders;
using Orders.Infrastructure.Services.Interfaces;

namespace Orders.Infrastructure.Handlers.PreOrders
{
    public class CreatePreOrderHandler : ServiceHandler, ICommandHandler<CreatePreOrder>
    {
        public CreatePreOrderHandler(IPreOrderService preOrderService) : base(preOrderService) { }

        public async Task HandleAsync(CreatePreOrder command)
            => await _preOrderService.CreateAsync(command.Name);
    }
}