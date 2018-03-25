using System.Threading.Tasks;
using Orders.Infrastructure.Commands.Interfaces;
using Orders.Infrastructure.Commands.PreOrders;
using Orders.Infrastructure.Services.Interfaces;

namespace Orders.Infrastructure.Handlers.PreOrders
{
    public class SavePreOrderHandler : ServiceHandler, ICommandHandler<SavePreOrder>
    {
        public SavePreOrderHandler(IPreOrderService preOrderService) : base(preOrderService) { }

        public async Task HandleAsync(SavePreOrder command)
            => await _preOrderService.SaveAsync(command.Name);
        
    }
}