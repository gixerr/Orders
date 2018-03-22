using System.Threading.Tasks;
using Orders.Infrastructure.Commands.Interfaces;
using Orders.Infrastructure.Commands.Orders;
using Orders.Infrastructure.Services.Interfaces;

namespace Orders.Infrastructure.Handlers.Orders
{
    public class CreateOrderHandler : ICommandHandler<CreateOrder>
    {
        private readonly IPreOrderService _preOrderService;

        public CreateOrderHandler(IPreOrderService preOrderService)
        {
            _preOrderService = preOrderService;

        }
        public async Task HandleAsync(CreateOrder command)
        {
            await _preOrderService.Create(command.Name);
        }
    }
}