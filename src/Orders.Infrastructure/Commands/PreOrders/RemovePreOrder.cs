using Orders.Infrastructure.Commands.Interfaces;

namespace Orders.Infrastructure.Commands.PreOrders
{
    public class RemovePreOrder : ICommand
    {
        public string Name { get; set; }
    }
}