using Orders.Infrastructure.Commands.Interfaces;

namespace Orders.Infrastructure.Commands.Orders
{
    public class AddOrder : ICommand
    {
        public string Name { get; set; }
    }
}