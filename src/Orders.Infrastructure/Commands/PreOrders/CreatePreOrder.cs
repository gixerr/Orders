using Orders.Infrastructure.Commands.Interfaces;

namespace Orders.Infrastructure.Commands.PreOrders
{
    public class CreatePreOrder : ICommand
    {
        public string Name { get; set; }
    }
}